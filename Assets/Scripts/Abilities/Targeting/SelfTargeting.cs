using UnityEngine;

[CreateAssetMenu(fileName = "SelfTargeting", menuName = "ROGUE/TargetingStrategy/Self", order = 0)]
public class SelfTargeting : TargetingStrategy
{
    public override void OnStart(Ability ability, TargetingManager targetingManager)
    {
        Debug.Log("SelfTargeting");
        if (targetingManager.transform.TryGetComponent<IDamageable>(out var target))
        {
            Debug.Log($"target: {target.gameObject.name}");
            ability.Execute(target);
        }
    }
}