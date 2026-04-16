using UnityEngine;

namespace ROGUE.Abilities
{
    [CreateAssetMenu(fileName = "DamageEffect", menuName = "ROGUE/Effects/DamageEffect")]
    public class DamageEffect : EffectData
    {
        public override void Apply(IEffectTarget target, float value)
        {
            IDamageable damageable = target.gameObject.GetComponent<IDamageable>();
            if (damageable != null) damageable.TakeDamage(value);
            OnCompleted?.Invoke(this);
        }
    }
}