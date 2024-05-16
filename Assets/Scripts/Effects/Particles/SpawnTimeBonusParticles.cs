using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimeBonusParticles : MonoBehaviour
{
    [SerializeField]
    private TargetManager _targetManager;

    [SerializeField]
    private ParticleSystem _timeBonusNormal;
    [SerializeField]
    private ParticleSystem _timeBonusWaveClear;

    private void OnEnable()
    {
        _targetManager.OnTargetDestroy += SpawnTimeBonusParticle;
    }

    private void OnDisable()
    {
        _targetManager.OnTargetDestroy -= SpawnTimeBonusParticle;
    }

    private void SpawnTimeBonusParticle(TargetManager targetManager, DamageReceiver target)
    {
        Vector3 spawnPos = target.transform.position;

        SpawnParticlesOnDeath targetParticleSpawn = target.GetComponentInChildren<SpawnParticlesOnDeath>();
        if (targetParticleSpawn != null) spawnPos = targetParticleSpawn.ParticleOrigin.position;

        if (targetManager.TargetsDestroyedSinceWaveSpawn == targetManager.TargetsPerWave)
        {
            Instantiate(_timeBonusWaveClear.gameObject, spawnPos, Quaternion.identity);
        }
        else
        {
            Instantiate(_timeBonusNormal.gameObject, spawnPos, Quaternion.identity);
        }
    }
}
