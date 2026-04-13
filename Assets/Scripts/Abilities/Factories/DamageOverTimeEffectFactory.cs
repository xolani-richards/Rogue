using UnityEngine;

[CreateAssetMenu(fileName ="DMG_Over_Time_Factory", menuName = "ROGUE/Effects/Factories/DamageEffectOverTime")]
public class DamageOverTimeEffectFactory : DamageFactory {
    public float duration = 3f;
    public float tickInterval = 1f;
    public float damagePerTick = 5;

    public override IEffect<IDamageable> Create() {
        return new DamageOverTimeEffect {
            duration = duration, 
            tickInterval = tickInterval, 
            damagePerTick = damagePerTick
        };
    }
}

public abstract class DamageFactory : ScriptableObject, IEffectFactory<IDamageable>
{
    public abstract IEffect<IDamageable> Create();
}