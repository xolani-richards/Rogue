using UnityEngine;

public interface IEffectStrategy
{
    float Evaluate(Ability ability, TargetingManager user);
}

public abstract class EffectStrategyBase : ScriptableObject, IEffectStrategy
{
    public abstract float Evaluate(Ability ability, TargetingManager user);
}

[CreateAssetMenu(fileName = "FIXED_", menuName = "ROGUE/Abilities/EffectCalculations/Fixed")]
public class EffectStrategy: EffectStrategyBase
{
    [SerializeField] float value;
    public override float Evaluate(Ability ability, TargetingManager user) => value;
}