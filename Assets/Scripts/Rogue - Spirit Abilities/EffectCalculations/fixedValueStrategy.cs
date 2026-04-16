using ROGUE.Abilities;
using UnityEngine;

public class FixedValueStrategy : IEffectCalculationStrategy
{
    public override float Calculate(GameObject caster, float baseValue) => baseValue;
}