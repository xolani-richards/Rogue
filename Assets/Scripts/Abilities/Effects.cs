using System;
using UnityEngine;
using ImprovedTimers;

public interface IEffectFactory<TTarget> {
    IEffect<TTarget> Create();
}

public interface IEffect<TTarget> {
    void Apply(TTarget target);
    void Cancel();
    event Action<IEffect<TTarget>> OnCompleted;
}

// [Serializable]
// public class DamageEffectFactory : IEffectFactory<IDamageable> {
//     public int damageAmount = 10;

//     public IEffect<IDamageable> Create() {
//         return new DamageEffect { damageAmount = damageAmount };
//     }
// }

[Serializable]
public struct DamageEffect : IEffect<IDamageable> {
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

// [Serializable]
// public class DamageOverTimeEffectFactory : IEffectFactory<IDamageable> {
//     public float duration = 3f;
//     public float tickInterval = 1f;
//     public int damagePerTick = 5;

//     public IEffect<IDamageable> Create() {
//         return new DamageOverTimeEffect {
//             duration = duration, 
//             tickInterval = tickInterval, 
//             damagePerTick = damagePerTick
//         };
//     }
// }

[Serializable]
public struct DamageOverTimeEffect : IEffect<IDamageable> {
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