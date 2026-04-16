using System.Collections.Generic;
using ROGUE.Abilities;
using UnityEngine;

public class DummyTarget : MonoBehaviour, ITargetable, IDamageable, IEffectTarget
{
    [SerializeField] float hitpoints = 50f;
    [Header("Effects")]
        [SerializeField] List<EffectData> effects = new ();

    public void ApplyEffect(EffectData effect, float value)
    {
        effects.Add(effect);
            effect.OnCompleted += RemoveEffect;
            effect.Apply(this, value);
        }

    void RemoveEffect(EffectData effect)
    {
        effect.OnCompleted -= RemoveEffect;
        effects.Remove(effect);
    }

    public void TakeDamage(float amount)
    {
        if(hitpoints <= 0) return;
        hitpoints = Mathf.Clamp(hitpoints - amount, 0, hitpoints);
    }
}