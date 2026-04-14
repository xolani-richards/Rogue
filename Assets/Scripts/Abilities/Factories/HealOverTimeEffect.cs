using System;
using ImprovedTimers;
using UnityEngine;

[CreateAssetMenu(fileName ="Heal_Over_Time_Effect", menuName = "ROGUE/Effects/Factories/HealEffectOverTime")]
public class HealOverTimeEffect : DamageFactory {
    public float duration = 3f;
    public float tickInterval = 1f;
    public float healPerTick = 5;

    public override IEffect<IDamageable> Create(TargetingManager user) {
        return new HealOverTimeEffectData {
            duration = duration, 
            tickInterval = tickInterval, 
            healPerTick = healPerTick
        };
    }
}

[Serializable]
public struct HealOverTimeEffectData : IEffect<IDamageable> {
    public float duration;
    public float tickInterval;
    public float healPerTick;
    
    public event Action<IEffect<IDamageable>> OnCompleted;
    
    IntervalTimer timer;
    IHealable currentTarget;

    public void Apply(IDamageable target) {
        if(target is not IHealable) return;
        currentTarget = (IHealable)target;
        timer = new IntervalTimer(duration, tickInterval);
        timer.OnInterval = OnInterval;
        timer.OnTimerStop = OnStop;
        timer.Start();
    }
    
    void OnInterval() {
        Debug.Log("Called on interval");
        currentTarget?.AddHealth(healPerTick);
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