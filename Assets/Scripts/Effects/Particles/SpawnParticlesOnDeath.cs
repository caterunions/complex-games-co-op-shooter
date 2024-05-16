using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticlesOnDeath : MonoBehaviour
{
    [SerializeField]
    private DamageReceiver _damageReciever;

    [SerializeField]
    private Transform _particleOrigin;
    public Transform ParticleOrigin => _particleOrigin;
    [SerializeField]
    private ParticleSystem _particles;
    [SerializeField]
    private bool _spawnBasedOnSource;

    private void OnEnable()
    {
        if (_particleOrigin == null) _particleOrigin = transform;
        if (_damageReciever == null) _damageReciever = GetComponent<DamageReceiver>();

        _damageReciever.OnDeath += DeathFX;
    }

    private void OnDisable()
    {
        _damageReciever.OnDeath -= DeathFX;
    }

    private void DeathFX(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        if(_spawnBasedOnSource)
        {
            Quaternion direction = Quaternion.Inverse(dmgEvent.Source.transform.rotation);
            Instantiate(_particles, dmgEvent.Source.transform.position, direction);
        }
        else
        {
            Instantiate(_particles, ParticleOrigin.position, ParticleOrigin.rotation);
        }
    }
}
