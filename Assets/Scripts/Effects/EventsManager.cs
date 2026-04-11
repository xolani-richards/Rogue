using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedAbilityEffectItem
{
    public TimedAbilityEffect effect;
    public float duration;
    public float countDown;
    public TimedAbilityEffectItem(TimedAbilityEffect effect)
    {
        this.effect = effect;
        this.duration = effect.duration;
        this.countDown = effect.duration;
    }

    public void OnTick(float deltaTime)
    {
        if(deltaTime <= 0) return;
        this.countDown -= deltaTime;
    }
}

public class EffectsManager:MonoBehaviour
{
    public List<AbilityEffect> effects = new ();
    public List<TimedAbilityEffectItem> timedEffects = new ();
    [SerializeField] bool isRunning = false;

    public void AddEffect(AbilityEffect effect)
    {
        effects.Add(effect);
        if(effect is TimedAbilityEffect) 
        {
            timedEffects.Add(new TimedAbilityEffectItem(effect as TimedAbilityEffect));
            if(!isRunning) StartCoroutine(OnTick());
        }
    }

    void RemoveEffect(AbilityEffect effect)
    {
        effects.Remove(effect);
    }

    IEnumerator OnTick()
    {
        isRunning = true;
        while(timedEffects.Count > 0)
        {
            for (int i = timedEffects.Count - 1; i >= 0 ; i--)
            {
                timedEffects[i].OnTick(Time.deltaTime);
                if(timedEffects[i].duration > 0) continue;
                RemoveEffect(timedEffects[i].effect);
                timedEffects.RemoveAt(i);
            }
            yield return null;
        }
        isRunning = false;
    }
}