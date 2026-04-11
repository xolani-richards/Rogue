using UnityEngine;

[CreateAssetMenu(fileName = "BaseStats", menuName = "Stats/BaseStats")]
public class BaseStats : ScriptableObject {
    [field: SerializeField] public float meleeAttack { get; protected set; } = 10f;
    [field: SerializeField] public float meleeDefense { get; protected set; } = 20f;
    [field: SerializeField] public float magicAttack { get; protected set; } = 10f;
    [field: SerializeField] public float magicDefense { get; protected set; } = 20f;
    [field: SerializeField] public float staminaRecovery { get; protected set;} = 1f;
    [field: SerializeField] public float maxHealth { get; protected set;} = 50f;
}