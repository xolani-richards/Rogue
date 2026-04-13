using System.Collections.Generic;
using UnityEngine;
using ROGUE.Characters;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof (Rigidbody))]
public class WeaponComponent: MonoBehaviour
{
    [SerializeField] List<IDamageable> hitBuffer = new ();
    [SerializeField] float baseDamage;
    [SerializeField] Collider hurtBox;
    [SerializeField] Character user;

    void Awake()
    {
        hurtBox = GetComponent<Collider>();
        hurtBox.enabled = false;
        hurtBox.isTrigger = true;
        Character character = GetComponentInParent<Character>();
        if(character != null) Bind(character); 
    }

    public void Bind(Character user) => this.user = user;

    public void SetActive(bool flag)
    {
        if (flag) hurtBox.enabled = true;
        else {
            hurtBox.enabled = false;
            hitBuffer.Clear ();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.IsChildOf(user?.transform)) return;
        IDamageable damageable = other.GetComponentInParent<IDamageable>();
        if (damageable != null && !hitBuffer.Contains(damageable)) OnHit(damageable);
    }

    private void OnHit(IDamageable damageable)
    {
        Debug.Log($"HIT: {damageable.gameObject.name}");
        GameObject caster = user == null ? null : user.gameObject;
        float damage = user == null ? baseDamage : user.stats.MeleeAttack;
        damageable.DoDamage(caster, damage);
    }
}