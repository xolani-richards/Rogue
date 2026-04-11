using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace ROGUE.Characters
{
    public class NPCMove: CharacterMove
    {
        [Header("State")]
        [SerializeField] Vector3 direction;
        NPC npc;

        public void SetSpeed(float speed) => targetSpeed = speed;

        protected override void Awake()
        {
            base.Awake();
            npc = GetComponent<NPC>();
            NavMeshAgent navAgent = GetComponent<NavMeshAgent>();
            navAgent.updatePosition = false;
            navAgent.updateRotation = false;
        }

        protected override void Update()
        {
            base.Update();
            npc.navAgent.nextPosition = transform.position;
        }

        protected override float CalculateSpeed ()
        {
            if(npc.navAgent.isStopped || npc.navAgent.remainingDistance <= npc.navAgent.stoppingDistance) targetSpeed = 0f;
            return base.CalculateSpeed();
        }

        protected override void Rotate()
        {
            Vector3 desiredMoveDirection = (direction - transform.position).normalized;
            if (desiredMoveDirection.magnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }
        }

        void FixedUpdate()
        {
            if(!npc.navAgent.hasPath) direction = Vector2.zero;
            else direction = npc.navAgent.steeringTarget;
        }

        void OnDrawGizmos()
        {
            Handles.DrawLine(transform.position, direction);
        }
    }
}