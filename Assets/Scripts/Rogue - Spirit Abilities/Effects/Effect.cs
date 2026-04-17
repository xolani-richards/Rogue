using System;
using UnityEngine;

namespace  ROGUE.Abilities
{
    public abstract class EffectData: ScriptableObject
    {
        public Action<EffectData> OnCompleted;
        public abstract void Apply(IEffectTarget target, float value);
        public virtual void Cancel()
        {}
    }

    [Serializable]
    public struct EffectInstance
    {
        public EffectData effect;
        public float baseValue;
        public IEffectCalculationStrategy calculationStrategy;
        public void Apply (GameObject caster, IEffectTarget target) {
            if(target == null) return;
            float str = calculationStrategy != null ? calculationStrategy.Calculate(caster, baseValue) : baseValue;
            EffectData instance = GameObject.Instantiate(effect);
            target.ApplyEffect(instance, str);
        }
    }
}