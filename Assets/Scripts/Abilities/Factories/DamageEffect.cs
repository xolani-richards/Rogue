using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Damage_Effect", menuName = "ROGUE/Effects/Factories/DamageEffect")]
public class DamageEffect : DamageFactory {
    public float damageAmount = 10;

    public override IEffect<IDamageable> Create(TargetingManager user) {
        return new DamageEffectData { damageAmount = damageAmount };
    }
}

[Serializable]
public struct DamageEffectData : IEffect<IDamageable> {
    public float damageAmount;
    
    public event Action<IEffect<IDamageable>> OnCompleted;

    public void Apply(IDamageable target) {
        target.TakeDamage(damageAmount);
        OnCompleted?.Invoke(this);
    }

    public void Cancel() {
        OnCompleted?.Invoke(this);
    }
}