using System;
using UnityEngine;

public interface IEffectFactory<TTarget> {
    IEffect<TTarget> Create(TargetingManager user);
}

public interface IEffect<TTarget> {
    void Apply(TTarget target);
    void Cancel();
    event Action<IEffect<TTarget>> OnCompleted;
}


public abstract class DamageFactory : ScriptableObject, IEffectFactory<IDamageable>
{
    [SerializeField] EffectStrategyBase effectStrategy;
    public abstract IEffect<IDamageable> Create(TargetingManager user);
}