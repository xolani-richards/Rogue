using UnityEngine;

namespace ROGUE.Abilities
{
    [CreateAssetMenu(fileName = "SelfTargeting", menuName = "ROGUE/Targeting/Self", order = 0)]
    public class SelfTargeting : TargetingStrategy
    {
        public override void OnStart(Ability ability, TargetingManager targetingManager)
        {
            if (targetingManager.transform.TryGetComponent<IEffectTarget>(out var target))
            {
                ability.Execute(targetingManager.gameObject, target);
            }
        }
    }
}