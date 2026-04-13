using System;
using UnityEngine;

public class AbilityHitTrigger: MonoBehaviour
{
    Collider _collider;
    GameObject _user;
    Action<GameObject> _func;
    bool _canHitSelf;
    bool _selfOnly;

    void Awake()
    {
        _collider = GetComponent<Collider>();
        if (_collider != null) _collider.enabled = false;
    }

    public virtual void Bind(GameObject user, float ttl, Action<GameObject> callback, bool canHitSelf = false, bool selfOnly = false)
    {
        _user = user;
        _func = callback;
        _collider.enabled = true;
        _canHitSelf = canHitSelf;
        _selfOnly = selfOnly;
        Destroy(gameObject, ttl);
    }

    void OnTriggerEnter(Collider other)
    {
        if (_user == null) { Debug.Log($"{name}: User not set!"); return; }
    
        IDamageable damageable = other.GetComponentInParent<IDamageable>();
        if(isValidTarget(damageable)) _func(damageable.gameObject);
    }

    bool isValidTarget(IDamageable damageable)
    {
        if (damageable == null) return false;
        else if (_selfOnly && damageable.gameObject != _user) return false;
        else if(damageable.gameObject == _user && !_canHitSelf) return false;
        return true;
    }
}