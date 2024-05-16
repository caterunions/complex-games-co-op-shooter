using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonusAudio : MonoBehaviour
{
    [SerializeField]
    private TargetManager _targetManager;

    [SerializeField]
    private AudioPlayer _timeBonusNormal;
    [SerializeField]
    private AudioPlayer _timeBonusWaveClear;

    private void OnEnable()
    {
        _targetManager.OnTargetDestroy += PlayTimeBonusAudio;
    }

    private void OnDisable()
    {
        _targetManager.OnTargetDestroy -= PlayTimeBonusAudio;
    }

    private void PlayTimeBonusAudio(TargetManager targetManager, DamageReceiver target)
    {
        if (targetManager.TargetsDestroyedSinceWaveSpawn == targetManager.TargetsPerWave)
        {
            _timeBonusWaveClear.Play();
        }
        else
        {
            _timeBonusNormal.Play();
        }
    }
}
