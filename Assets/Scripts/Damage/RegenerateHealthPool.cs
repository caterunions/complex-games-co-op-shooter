using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerateHealthPool : MonoBehaviour
{
    [SerializeField]
    private DamageReceiver _damageReceiver;
    [SerializeField]
    private HealthPool _healthPool;
    [SerializeField]
    private float _rechargeDelay = 5f;
    [SerializeField]
    private float _timeToFullRecharge = 2f;

    private float _timeSinceLastDamage;

    private void OnEnable()
    {
        _damageReceiver.OnDamage += ResetRegenCooldown;
    }

    private void OnDisable()
    {
        _damageReceiver.OnDamage -= ResetRegenCooldown;
    }

    private void ResetRegenCooldown(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        if (result.DamageTaken > 0f) _timeSinceLastDamage = Time.time;
    }

    private void Update()
    {
        if(_healthPool.HP < _healthPool.MaxHP && Time.time - _timeSinceLastDamage >= _rechargeDelay)
        {
            _healthPool.Heal((_healthPool.MaxHP * Time.deltaTime) / _timeToFullRecharge);
        }
    }
}
