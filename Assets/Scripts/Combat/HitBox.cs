using System;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    Collider _collider;
    GameObject _user;
    Action<GameObject> _func;

    void Awake()
    {
        _collider = GetComponent<Collider>();
        if (_collider != null) _collider.enabled = false;
    }

    public void Bind(GameObject user, float ttl, Action<GameObject> callback)
    {
        _user = user;
        _func = callback;
        _collider.enabled = true;
        Destroy(gameObject, ttl);
    }

    void OnTriggerEnter(Collider other)
    {
        if (_user == null) { Debug.Log($"{name}: User not set!"); return; }
        if (other.transform.IsChildOf(_user.transform)) return;

        IDamageable damageable = other.GetComponentInParent<IDamageable>();
        if(damageable != null) _func(damageable.gameObject);
    }
}