using UnityEngine;
using UnityEngine.AI;

namespace ROGUE.Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NPC : Character
    {
        [SerializeField] AnimationClip idleAnim;
        [SerializeField] AnimationClip walkAnim;
        [SerializeField] AnimationClip runAnim;
        [SerializeField] bool randomiseSpeed = true;

        [field: SerializeField] public float attackDistance { get; protected set; }
        public NavMeshAgent navAgent;
        public NPCMove move;
        public override void OnAccept(IVisitor visitor) => visitor.OnNPCVisit();

        protected override void Awake()
        {
            base.Awake();
            animationSystem = new (animator, idleAnim, walkAnim, runAnim, randomiseSpeed);
            animationSystem.UpdateLocomotion(0f);
            navAgent = GetComponent<NavMeshAgent>();
            move = GetComponent<NPCMove>();
        }

        public override void OnDied()
        {
            animationSystem.PlayOneShotAndDestroy(deathAnim);
            this.controller.enabled = false;
            base.OnDied();
        }
    }
}