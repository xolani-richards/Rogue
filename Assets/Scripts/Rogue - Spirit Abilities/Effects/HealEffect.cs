using UnityEngine;

namespace ROGUE.Abilities
{
    [CreateAssetMenu(fileName = "HealEffect", menuName = "ROGUE/Effects/HealEffect")]
    public class HealEffect : EffectData
    {
        public override void Apply(IEffectTarget target, float value)
        {
            IHealable healable = target.gameObject.GetComponent<IHealable>();
            if (healable != null) healable.AddHealth(value);
            OnCompleted?.Invoke(this);
        }
    }
}