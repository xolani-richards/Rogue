using UnityEngine;

[CreateAssetMenu(fileName ="DMG_Factory", menuName = "ROGUE/Effects/Factories/DamageEffect")]
public class DamageEffectFactory : DamageFactory {
    public float damageAmount = 10;

    public override IEffect<IDamageable> Create() {
        return new DamageEffect { damageAmount = damageAmount };
    }
}