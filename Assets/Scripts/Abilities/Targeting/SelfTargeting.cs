using UnityEngine;

[CreateAssetMenu(fileName = "SelfTargeting", menuName = "ROGUE/TargetingStrategy/Self", order = 0)]
public class SelfTargeting : TargetingStrategy
{
    public override void OnStart(Ability ability, TargetingManager targetingManager)
    {
        if (targetingManager.transform.TryGetComponent<IDamageable>(out var target))
        {
            ability.Execute(target);
        }
    }
}