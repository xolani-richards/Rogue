using System;
using System.Collections.Generic;
using ROGUE.Abilities;
using UnityEngine;
using UnityEngine.Events;

namespace ROGUE.Characters
{
[RequireComponent(typeof(Health))]
    public abstract class Character : MonoBehaviour, IActor, IDamageable, ITargetable, IEffectTarget
    {
        [Header("Animation")]

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
        [field: SerializeField] public Stats stats { get; private set; } = null;

        [Header("Flags")]
        public bool isExecuting;
        public bool canMove;
        public bool canRotate;
        public bool useRootMotion = false;

        [Header("Effects")]
        [SerializeField] List<EffectData> effects = new ();

        public Action onDied;

        protected CharacterController controller;

        protected virtual void Awake()
        {
            context = new();
            stats = new Stats(new StatsMediator(), baseStats);

            controller = GetComponent<CharacterController>();
            health = GetComponent<Health>();
            health.Bind(this);
            
            animator = GetComponentInChildren<Animator>();
            sensor = GetComponentInChildren<Sensor>();
            animationController = GetComponentInChildren<AnimationController>();
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
            if(health.hitpoints <= 0) return;

            health.RemoveHealth(baseValue);
            context.isTakingDamage = true;
            // context.SetData("TakingDamage", 1f);
        }

        protected virtual void OnDied ()
        {
            context.SetData("Dead", 1f);
            effects.ForEach(effect => effect.Cancel());
            onDied?.Invoke();
        }

        public void ApplyEffect(EffectData effect, float value)
        {
            effects.Add(effect);
            effect.OnCompleted += RemoveEffect;
            effect.Apply(this, value);
        }

        void RemoveEffect(EffectData effect)
        {
            effect.OnCompleted -= RemoveEffect;
            effects.Remove(effect);
        }
    }
}