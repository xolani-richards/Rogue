using UnityEngine;

namespace ROGUE.Characters
{
    public class TwinStickMove : CharacterMove
    {
        [Header("Aim target")]
        [SerializeField] PlayerAimTarget aimTarget;
        [SerializeField, Range(0, 5f)] float minAimDistance = 0.5f;

        protected override float CalculateSpeed()
        {
            if (!character.canMove) targetSpeed = 0f;
            else
            {
                targetSpeed = moveInput.y > 0.01f ? 1 : 0;
                if (isWalking && targetSpeed > 0) targetSpeed = 0.5f;
            }

            return base.CalculateSpeed();
        }

        protected override void OnMove ()
        {
            base.OnMove();
            character.animator.SetFloat("Strafe", moveInput.x);
        }

        protected override void Rotate ()
        {
            if(!character.canRotate) return;
            float distance = Vector3.Distance(transform.position, aimTarget.transform.position);
            if(distance < minAimDistance) return;

            Vector3 lookDirection = aimTarget.transform.position - transform.position;
            lookDirection.y = 0f;
            lookDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        }

    }
}