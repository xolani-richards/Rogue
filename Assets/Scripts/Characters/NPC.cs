using UnityEngine;
using UnityEngine.AI;

namespace ROGUE.Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NPC : Character
    {
        [field: SerializeField] public float attackDistance { get; protected set; }
        public NavMeshAgent navAgent;
        public NPCMove move;
        public override void OnAccept(IVisitor visitor) => visitor.OnNPCVisit();

        protected override void Awake()
        {
            base.Awake();
            navAgent = GetComponent<NavMeshAgent>();
            move = GetComponent<NPCMove>();
        }

        public override void OnDied()
        {
            this.controller.enabled = false;
            canMove = false;
            canRotate = false;
            animationSystem.PlayOneShotAndDestroy(deathAnim);
            onDied?.Invoke();
        }
    }
}