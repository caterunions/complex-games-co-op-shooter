using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public event Action<Projectile, Collision> OnHit;

    public float Damage { get; set; } = 0f;

    [SerializeField]
    private bool _destroyOnCollision = true;
    [SerializeField]
    private EventOnLifetime _eventOnLifetime;

    private void OnEnable()
    {
        if (_eventOnLifetime != null) _eventOnLifetime.OnLifetime += OnLifetime;
    }

    private void OnDisable()
    {
        if (_eventOnLifetime != null) _eventOnLifetime.OnLifetime -= OnLifetime;
    }

    public void RequestDestroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision coll)
    {
        OnHit?.Invoke(this, coll);
        DamageReceiver dr = coll.gameObject.GetComponentInParent<DamageReceiver>();
        if (dr != null)
        {
            dr.ReceiveDamage(new DamageEvent(Damage, gameObject, coll));
        }
        if(_destroyOnCollision) RequestDestroy();
    }

    private void OnLifetime(EventOnLifetime onLifetime)
    {
        RequestDestroy();
    }
}