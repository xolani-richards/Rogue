using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Heal_Instant_Effect", menuName = "ROGUE/Effects/Factories/HealEffect")]
public class InstantHealEffect : DamageFactory {
    public float healAmount = 10;

    public override IEffect<IDamageable> Create(TargetingManager user) {
        return new InstantHealEffectData { healAmount = healAmount };
    }
}

[Serializable]
public struct InstantHealEffectData : IEffect<IDamageable> {
    public float healAmount;
    
    public event Action<IEffect<IDamageable>> OnCompleted;

    public void Apply(IDamageable target) {
        if(target is not IHealable) return;
        IHealable healable= (IHealable)target;
        healable.AddHealth(healAmount);
        OnCompleted?.Invoke(this);
    }

    public void Cancel() {
        OnCompleted?.Invoke(this);
    }
}