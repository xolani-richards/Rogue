using UnityEngine;

namespace ROGUE.Characters
{
    public class HeroMove: CharacterMove
    {
        [SerializeField] Vector2 moveInput;
        [SerializeField] bool isWalking;
    
        public void SetMoveInput(Vector2 moveInput) => this.moveInput = moveInput;
        public void IsWalking(bool flag) => this.isWalking = flag;

        protected override float CalculateSpeed()
        {
            if(!character.canMove) targetSpeed = 0f;
            else
            {
                targetSpeed = moveInput.y > 0.01f ? 1: 0;
                if (isWalking && targetSpeed > 0) targetSpeed = 0.5f;
            }
            
            return base.CalculateSpeed();
        }

        protected override void Rotate()
        {
            if(!character.canRotate) return;
            // if want this to be relative to Camera, change these to the camera transform
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            Vector3 desiredMoveDirection = forward * moveInput.y + right * moveInput.x;
            desiredMoveDirection.Normalize();

            if (desiredMoveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }
        }
    }
}