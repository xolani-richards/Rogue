using UnityEngine;

namespace ROGUE.Abilities
{
    public abstract class IEffectCalculationStrategy: ScriptableObject
    {
        public abstract float Calculate(GameObject caster, float baseValue);
    }
}