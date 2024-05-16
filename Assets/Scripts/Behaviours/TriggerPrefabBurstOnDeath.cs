using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPrefabBurstOnDeath : MonoBehaviour
{
    [SerializeField]
    private DamageReceiver _damageReceiver;
    [SerializeField]
    private PrefabBurst _burst;
    [SerializeField]
    private int _count;
    [SerializeField]
    private float _radius;

    private void OnEnable()
    {
        if (_burst == null) _burst = GetComponent<PrefabBurst>();
        if (_damageReceiver == null) _damageReceiver = GetComponent<DamageReceiver>();

        _damageReceiver.OnDeath += SpawnBurst;
    }

    private void OnDisable()
    {
        _damageReceiver.OnDeath -= SpawnBurst;
    }

    private void SpawnBurst(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        _burst.CreateBurst(_count, _radius);
    }
}
