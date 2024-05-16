using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticlesOnHit : MonoBehaviour
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
    [SerializeField]
    private bool _playOnDeath = false;

    private void OnEnable()
    {
        if (_particleOrigin == null) _particleOrigin = transform;
        if (_damageReciever == null) _damageReciever = GetComponent<DamageReceiver>();

        _damageReciever.OnDamage += HitFX;
    }

    private void OnDisable()
    {
        _damageReciever.OnDamage -= HitFX;
    }

    private void HitFX(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        if(_playOnDeath)
        {
            SpawnParticles(dmgEvent.Collision);
        }
        else if(!result.Killed)
        {
            SpawnParticles(dmgEvent.Collision);
        }
    }

    private void SpawnParticles(Collision source)
    {
        if (_spawnBasedOnSource && source != null)
        {
            ContactPoint contact = source.GetContact(0);
            Vector3 spawnPos = contact.point;
            Quaternion direction = contact.thisCollider.transform.rotation * Quaternion.Euler(0f, 180f, 0f);
            Instantiate(_particles, spawnPos, direction);
        }
        else
        {
            Instantiate(_particles, ParticleOrigin.position, ParticleOrigin.rotation);
        }
    }
}
