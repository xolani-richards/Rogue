using UnityEngine;

public class CharacterController: MonoBehaviour
{
    [field: SerializeField] public Vector2 moveInput { get; protected set; }
    [field: SerializeField] public bool attackInput { get; set; }
    [field: SerializeField] public bool walkInput { get; protected set; }
    protected bool meleeAttack;
}