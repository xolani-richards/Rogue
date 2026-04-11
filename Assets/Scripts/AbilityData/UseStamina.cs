using UnityEngine;

[CreateAssetMenu(fileName = "useStamina", menuName = "ROGUE/Effects/Use Stamina")]
public class UseStamina : AbilityEffect
{
    public override void Execute(GameObject caster, GameObject target, float cost)
    {
        // IStamina stamina = caster.GetComponentInParent<IStamina>();
        // if (stamina != null) stamina.UseStamina(cost);
    }
}