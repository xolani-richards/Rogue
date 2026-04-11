using System.Collections;
using UnityEngine;
using ROGUE.Characters;

public class HeroAttack : MonoBehaviour
{
    [SerializeField] AbilityData ability;
    [SerializeField] bool CanAttack = true;
    // [SerializeField] Weapon weapon;
    [SerializeField] float fadeAmount = 0.01f;

    // HeroMove _heroMove;
    // Controls _controls;
    Animator _animator;

    void Awake()
    {
        // _animator = GetComponent<Animator>();
        // _heroMove = GetComponent<HeroMove>();

        // _controls = new();
        // _controls.Player.Attack.performed += ctx => OnAttack();
        // _controls.Player.Enable();
        // if(weapon != null) OnWeaponEquiped(weapon);
    }

    // void OnDestroy() => _controls.Player.Disable();

    void OnAttack()
    {
        if (!CanAttack) { Debug.Log("Can't attack!"); return; }
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
        CanAttack = false;
        // _heroMove.SetCanMove(false);
        float countdown = ability.castTime;
        float currentAnimSpeed = _animator.speed;
        _animator.speed = ability.playbackSpeed;
        _animator.CrossFade(ability.clip.name, fadeAmount);

        while (countdown > 0f)
        {
            countdown -= Time.deltaTime;
            yield return null;
        }
        // if (weapon != null) weapon.SetActive(true);
        // else SpawnFX();
        yield return new WaitForSeconds(ability.duration);
        CleanUp(currentAnimSpeed);
    }

    void CleanUp (float currentAnimSpeed)
    {
        // _heroMove.SetCanMove(true);
        // if (weapon != null) weapon.SetActive(false);        
        _animator.speed = currentAnimSpeed;
        CanAttack = true;
    }
}