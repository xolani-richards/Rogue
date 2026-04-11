using ROGUE.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatTest: MonoBehaviour
{
    [SerializeField] StatType type = StatType.MeleeAttack;
    [SerializeField] OperatorType operatorType = OperatorType.Add;
    [SerializeField] float value = 10;
    [SerializeField] float duration = 5f;

    StatModifierFactory factory = new ();

    void ApplyModifier(Character character)
    {
        StatModifier modifier = factory.Create(operatorType, type, value, duration);
        character.stats.Mediator.AddModifier(modifier);
    }

    void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponentInParent<Character>();
        if(character == null) return;
        ApplyModifier(character);
        Destroy(gameObject, 0.2f);
    }
}