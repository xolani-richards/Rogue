using UnityEngine;
using UnityEngine.InputSystem;
using ROGUE.Characters;

public class TestDamage: MonoBehaviour
{
    [SerializeField] AbilityData ability;
    public int damage;
    [SerializeField] Character target;

    // void OnTriggerEnter(Collider other)
    // {
    //     IDamageable target = other.GetComponentInParent<IDamageable>();
    //     if (target != null) ability.effects.ForEach(effect => effect.Execute(gameObject, target.gameObject));
    // }

    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame) Execute();
    }

    void Execute()
    {
        // target.DoDamage(gameObject, damage);
        ability.effects.ForEach(effect => effect.Execute(gameObject, target.gameObject));
    }
}