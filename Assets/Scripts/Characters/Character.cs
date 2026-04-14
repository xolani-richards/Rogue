using UnityEngine;
using UnityEngine.Events;

namespace ROGUE.Characters
{
[RequireComponent(typeof(Health))]
    public abstract class Character : MonoBehaviour, IActor, IDamageable, ITargetable
    {
        [Header("Animation")]
        [SerializeField] AnimationClip idleAnim;
        [SerializeField] AnimationClip walkAnim;
        [SerializeField] AnimationClip runAnim;
        [SerializeField] bool randomiseSpeed = true;
        [SerializeField] public AnimationClip deathAnim;
        [SerializeField] public AnimationClip damageAnim;

        [HideInInspector] public Health health;
        [HideInInspector] public Character character => this;
        [HideInInspector] public Sensor sensor;
        [HideInInspector] public Animator animator;
        [HideInInspector] public AnimationController animationController;
        [HideInInspector] public AnimationSystem animationSystem;

        [Header("Context")]
        public Context context;

        [Header("Stats")]
        [SerializeField] BaseStats baseStats;
        [field: SerializeField] public Stats stats { get; private set; }

        [Header("Flags")]
        public bool isExecuting;
        public bool canMove;
        public bool canRotate;
        public bool useRootMotion = false;

        [Header("Events")]
        public UnityEvent onTakeHit;
        public UnityEvent onDied;
        protected CharacterController controller;
        

        protected virtual void Awake()
        {
            context = new();
            stats = new Stats(new StatsMediator(), baseStats);

            controller = GetComponent<CharacterController>();
            health = GetComponent<Health>();
            animator = GetComponentInChildren<Animator>();
            sensor = GetComponentInChildren<Sensor>();
            animationController = GetComponentInChildren<AnimationController>();
            animationSystem = new (animator, idleAnim, walkAnim, runAnim, randomiseSpeed);
            animationSystem.UpdateLocomotion(0f);
            // animationSystem.PlayOneShot(idleAnim);
            health.died += OnDied;
        }

        protected virtual void Update()
        {
            stats.Mediator.Update(Time.deltaTime);
        }

        protected virtual void OnDestroy()
        {
            animationSystem.Destroy();
            health.died -= OnDied;
        }

        public abstract void OnAccept(IVisitor visitor);

        public void TakeDamage(float baseValue)
        {
            Debug.Log($"Taking damage: {baseValue}");
            if(health.health <= 0) return;
            health.RemoveHealth(baseValue);
            context.SetData("TakingDamage", 1f);
            onTakeHit?.Invoke();
            return;
        }

        public void ApplyEffect(IEffect<IDamageable> effect) => effect.Apply(this);
        

        public virtual void OnDied ()
        {
            context.SetData("Dead", 1f);
        }
    }
}