using System.Collections;
using System.Collections.Generic;
using ROGUE.Characters;
using UnityEngine;

public class MeleeAttack: Attack
{
    [Header("Melee")]
    [SerializeField] List<AttackData> attacks = new ();
    [SerializeField] int index;
    
    WeaponComponent weaponComponent;
    Stamina stamina;

    float countDown;

    protected override void Start()
    {
        base.Start();
        character.animationController.onAnimEvent += OnAnimEvent;
        weaponComponent = GetComponentInChildren<WeaponComponent> (); 
        stamina = character.GetComponent<Stamina>();  
    }

    void OnDestroy()
    {
        character.animationController.onAnimEvent -= OnAnimEvent;
    }

    public void OnInterupt() => Reset();

    void UpdateIndex ()
    {
        index += 1;
        if(index >= attacks.Count) index = 0;
    }

    void Reset()
    {
        index = 0;
        attackQueued = false;
        canAttack = true;
        canQueueAttack = true;
        isExecuting = false;
        weaponComponent.SetActive(false);
    }

    protected override void StartAttack()
    {
        AttackData attack = attacks [index];
        float offset = index == 0 ? 0: attack.blendDelay;
        countDown = (attack.clip.length - offset) / attack.playbackSpeed;
        character.animationSystem.PlayOneShot (attack.clip, attack.playbackSpeed, offset);
        if(stamina != null) stamina.RemoveStamina(attack.baseStaminaCost);
        if(!isExecuting) StartCoroutine(CountDown());

        UpdateIndex();
        canAttack = false;
        canQueueAttack = true;
        attackQueued = false;
    }

    void OnCombo ()
    {
        if(!attackQueued) return;
        StartAttack();
    }

    void OnAnimEvent(string @event)
    {
        switch (@event)
        {
            case "WeaponActive":
                weaponComponent?.SetActive (true);
                break;

            case "WeaponDeactive":
                weaponComponent?.SetActive (false);
                break;

            case "StartCombo":
                OnCombo();
                break;
        }
    }

    IEnumerator CountDown()
    {
        isExecuting = true;
        while(countDown > 0)
        {
            countDown -= Time.deltaTime;
            yield return null;
        }
        Reset();
    }

}