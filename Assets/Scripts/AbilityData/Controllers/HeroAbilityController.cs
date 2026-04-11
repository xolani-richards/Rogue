using System;
using System.Collections;
using System.Collections.Generic;
using ROGUE.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class AbilityItem
{
    public AbilityData ability;
    public bool isExecuting;
    public float cooldown = 0f;
}

public class HeroAbilityController: MonoBehaviour
{
    public List<AbilityItem> abilities = new ();
    [SerializeField] AbilityData currentAbility;
    [SerializeField] bool isExecuting = false;

    public void Update()
    {
        if (isExecuting) {}
        else if(Keyboard.current.eKey.wasPressedThisFrame) Execute(0);
        else if(Keyboard.current.rKey.wasPressedThisFrame) Execute(1);
        else if(Keyboard.current.tKey.wasPressedThisFrame) Execute(2);
    }

    void Execute(int index)
    {
        if(isExecuting) return;
        if(index >= abilities.Count) return;
        StartCoroutine(Process(abilities[index]));
    }

    IEnumerator Process(AbilityItem abilityItem)
    {
        bool hasCast = false;
        isExecuting = true;
        currentAbility = abilityItem.ability;
        float duration = currentAbility.clip.length;
        float timer = 0f;
        Hero.instance.animationSystem.PlayOneShot(currentAbility.clip);
        
        while(timer < duration)
        {
            if(!hasCast && timer >= currentAbility.castTime)
            {
                hasCast = true;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        isExecuting = false;
    }

}