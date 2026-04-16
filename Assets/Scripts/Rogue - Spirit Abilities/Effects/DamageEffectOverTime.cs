using UnityEngine;
using MEC;
using System.Collections.Generic;

namespace ROGUE.Abilities
{
    [CreateAssetMenu(fileName = "DamageEffectOverTime", menuName = "ROGUE/Effects/DamageEffectOverTime")]
    public class DamageEffectOverTime : EffectData
    {
        [SerializeField] float DamagePerTick;
        [SerializeField] float Duration;
        [SerializeField] float TickInterval;

        CoroutineHandle handle;

        public override void Apply(IEffectTarget target, float value)
        {
            Duration = value;
            IDamageable damageable = target.gameObject.GetComponent<IDamageable>();
            if (damageable != null) handle = Timing.RunCoroutine(Run(damageable));
        }

        public override void Cancel ()
        {
            Timing.KillCoroutines(handle);
            Cleanup();
        }

        void Cleanup() {
            OnCompleted?.Invoke(this);
        }

        IEnumerator<float> Run (IDamageable damageable)
        {
            float timer = 0;
            while (timer <= Duration)
            {
                damageable.TakeDamage(DamagePerTick);
                yield return Timing.WaitForSeconds(TickInterval);
                timer += TickInterval;
            }
            Cleanup();
        }
    }
}