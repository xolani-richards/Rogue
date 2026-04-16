using UnityEngine;
using MEC;
using System.Collections.Generic;

namespace ROGUE.Abilities
{
    [CreateAssetMenu(fileName = "HealEffectOverTime", menuName = "ROGUE/Effects/HealEffectOverTime")]
    public class HealEffectOverTime : EffectData
    {
        [SerializeField] float healthPerTick;
        [SerializeField] float duration;
        [SerializeField] float tickInterval;

        CoroutineHandle handle;

        public override void Apply(IEffectTarget target, float value)
        {
            duration = value;
            IHealable healable = target.gameObject.GetComponent<IHealable>();
            if (healable != null) handle = Timing.RunCoroutine(Run(healable));
        }

        public override void Cancel ()
        {
            Timing.KillCoroutines(handle);
            Cleanup();
        }

        void Cleanup() {
            OnCompleted?.Invoke(this);
        }

        IEnumerator<float> Run (IHealable target)
        {
            float timer = 0;
            while (timer <= duration)
            {
                target.AddHealth(healthPerTick);
                yield return Timing.WaitForSeconds(tickInterval);
                timer += tickInterval;
            }
            Cleanup();
        }
    }
}