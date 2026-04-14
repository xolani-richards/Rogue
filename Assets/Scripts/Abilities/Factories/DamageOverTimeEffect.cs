using System;
using ImprovedTimers;
using UnityEngine;

[CreateAssetMenu(fileName ="DMG_Over_Time_Effect", menuName = "ROGUE/Effects/Factories/DamageEffectOverTime")]
public class DamageOverTimeEffect : DamageFactory {
    public float duration = 3f;
    public float tickInterval = 1f;
    public float damagePerTick = 5;

    public override IEffect<IDamageable> Create(TargetingManager user) {
        return new DamageOverTimeEffectData {
            duration = duration, 
            tickInterval = tickInterval, 
            damagePerTick = damagePerTick
        };
    }
}

[Serializable]
public struct DamageOverTimeEffectData : IEffect<IDamageable> {
    public float duration;
    public float tickInterval;
    public float damagePerTick;
    
    public event Action<IEffect<IDamageable>> OnCompleted;
    
    IntervalTimer timer;
    IDamageable currentTarget;

    public void Apply(IDamageable target) {
        currentTarget = target;
        timer = new IntervalTimer(duration, tickInterval);
        timer.OnInterval = OnInterval;
        timer.OnTimerStop = OnStop;
        timer.Start();
    }
    
    void OnInterval() {
        Debug.Log("Called on interval");
        currentTarget?.TakeDamage(damagePerTick);
    }
    void OnStop() => Cleanup();

    public void Cancel() {
        timer?.Stop();
        Cleanup();
    }

    void Cleanup() {
        timer = null;
        currentTarget = null;
        OnCompleted?.Invoke(this);
    }
}