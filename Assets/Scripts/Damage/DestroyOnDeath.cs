using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField]
    private DamageReceiver _damageReceiver;

    private void OnEnable()
    {
        if(_damageReceiver == null) _damageReceiver = GetComponent<DamageReceiver>();

        _damageReceiver.OnDeath += DestroyThis;
    }

    private void OnDisable()
    {
        _damageReceiver.OnDeath -= DestroyThis;
    }

    private void DestroyThis(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        Destroy(gameObject);
    }
}
