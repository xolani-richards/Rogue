using UnityEngine;

[CreateAssetMenu(fileName = "DoDamage", menuName = "ROGUE/Effects/Do damage")]
public class DoDamage : AbilityEffect
{
    public override void Execute(GameObject caster, GameObject target, float damage)
    {
        IDamageable damageable = target.GetComponentInParent<IDamageable>();
        if (damageable != null) damageable.DoDamage(caster, damage);
    }
}