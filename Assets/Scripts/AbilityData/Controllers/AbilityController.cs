using System.Collections;
using UnityEngine;
using ROGUE.Characters;

public class AbilityController : MonoBehaviour
{
    [SerializeField] AbilityData ability;
    [SerializeField] bool canExecute;
    [SerializeField] float cooldown;

    Character character;

    void Awake()
    {
        character = GetComponent<Character>();
        canExecute = true;
    }

    public void OnExecute()
    {
        if (!canExecute) 
        { 
            Debug.Log("Can't use!"); 
            return; 
        }
        StartCoroutine(Execute());
    }

    // void OnWeaponEquiped(Weapon newWeapon)
    // {
    //     if (weapon != null) weapon.OnHit -= Onhit;
    //     weapon = newWeapon;
    //     weapon.OnHit += Onhit;
    // }

    void Onhit(IDamageable target)
    {
        if (target == null || target.gameObject == gameObject) { return; }
        ability.effects.ForEach(effect => effect.Execute(gameObject, target.gameObject));
    }

    void SpawnFX()
    {
        if (ability.vfx != null) Instantiate(ability.vfx, transform)
            .Bind(gameObject, ability.duration, (target) => Onhit(target.GetComponent<IDamageable>()));
    }

    IEnumerator Execute()
    {
        OnInit();
        
        float countdown = ability.castTime;
        while (countdown > 0f)
        {
            countdown -= Time.deltaTime;
            yield return null;
        }
        
        // if (weapon != null) weapon.SetActive(true);
        // else 
        SpawnFX();
        yield return new WaitForSeconds(ability.duration);
        
        character.canMove = true;
        character.canRotate = true;
        character.isExecuting = false;
        yield return CoolDown();
    }

    IEnumerator CoolDown ()
    {
        cooldown = ability.cooldown;
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        canExecute = true;
        cooldown = 0f;
    }

    void OnInit()
    {
        canExecute = false;
        character.canMove = ability.canMove;
        character.canRotate = ability.canRotate;
        character.isExecuting = true;

        character.animationSystem.PlayOneShot(ability.clip, ability.playbackSpeed);
        // character.animator.CrossFade(ability.clip.name, fadeAmount);
    }

    void Reset()
    {
        // if (weapon != null) weapon.SetActive(false);        

    }
}