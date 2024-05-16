using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrackTimeBonuses : MonoBehaviour
{
    [SerializeField]
    private TimeKeeper _timeKeeper;
    [SerializeField]
    private TargetManager _targetManager;
    [SerializeField]
    private BotManager _botManager;

    [SerializeField]
    private float _targetTimeValue = 2.5f;
    public float TargetTimeValue => _targetTimeValue;
    [SerializeField]
    private float _waveTimeValue = 10f;
    public float WaveTimeValue => _waveTimeValue;

    private void OnEnable()
    {
        _targetManager.OnTargetDestroy += TargetDestroyed;
        _botManager.OnBotDestroy += BotDestroyed;
    }

    private void OnDisable()
    {
        _targetManager.OnTargetDestroy -= TargetDestroyed;
        _botManager.OnBotDestroy -= BotDestroyed;
    }

    private void TargetDestroyed(TargetManager targetManager, DamageReceiver target)
    {
        if(targetManager.TargetsDestroyedSinceWaveSpawn == targetManager.TargetsPerWave)
        {
            _timeKeeper.AddBonusTime(_waveTimeValue);
        }
        else
        {
            _timeKeeper.AddBonusTime(_targetTimeValue);
        }
    }

    private void BotDestroyed(BotManager botManager, BotBrain brain, DamageReceiver dr)
    {
        _timeKeeper.AddBonusTime(brain.SpawnedPointValue / 8);
    }
}
