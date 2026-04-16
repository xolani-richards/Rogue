using UnityEngine;

namespace ROGUE.Characters
{
    public class CharacterMove : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] protected float turnSpeed;
        [SerializeField] protected float dampSpeed;
        [SerializeField] protected float moveThreshold = 0.01f;

        [Header("State")]
        [SerializeField] protected float targetSpeed;
        [SerializeField] protected float currentSpeed;

        [SerializeField] protected Vector2 moveInput;
        [field: SerializeField] public bool isWalking;
        protected Character character;
        protected float currentVelocity;

        protected virtual void Awake()
        {
            character = GetComponent<Character>();
        }
        protected virtual void Update()
        {
            OnMove();
            Rotate();
        }
        protected virtual float CalculateSpeed() => Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocity, dampSpeed);

        public void SetMoveInput(Vector2 moveInput) => this.moveInput = moveInput;
        public void IsWalking(bool flag) => this.isWalking = flag;

        protected virtual void OnMove()
        {
            currentSpeed = CalculateSpeed();
            if (currentSpeed <= moveThreshold) currentSpeed = 0f;
            character.animationSystem.UpdateLocomotion(currentSpeed);
        }

        protected virtual void Rotate()
        { }
    }
}