using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerTrackStatsAndScore : MonoBehaviour
{
    [SerializeField]
    //private PlayerFire _playerFire;
    private WeaponHolder _weaponHolder;
    [SerializeField]
    private StatsReveal _statsReveal;
    [SerializeField]
    private StatsKeeper _statsKeeper;
    [SerializeField]
    private ScoreKeeper _scoreKeeper;
    [SerializeField]
    private TargetManager _targetManager;
    [SerializeField]
    private BotManager _botManager;

    [SerializeField]
    private int _targetPointValue = 10;
    [SerializeField]
    private int _wavePointValue = 100;

    private void OnEnable()
    {
        //_playerFire.OnPlayerFireAnyWeapon += AddShot;
        _weaponHolder.OnWeaponChanged += WeaponChanged;

        _targetManager.OnTargetDestroy += TargetDestroyed;
        _botManager.OnBotDestroy += BotDestroyed;
    }
    private void OnDisable()
    {
        //_playerFire.OnPlayerFireAnyWeapon -= AddShot;
        _weaponHolder.OnWeaponChanged -= WeaponChanged;
        if (_weaponHolder.CurrentWeapon != null) _weaponHolder.CurrentWeapon.OnFireComplete -= AddShot;

        _targetManager.OnTargetDestroy -= TargetDestroyed;
        _botManager.OnBotDestroy -= BotDestroyed;
    }

    private void WeaponChanged( WeaponHolder weaponHolder, Weapon previousWeapon )
    {
        if (previousWeapon != null) previousWeapon.OnFireComplete -= AddShot;
        if (weaponHolder.CurrentWeapon != null) weaponHolder.CurrentWeapon.OnFireComplete += AddShot;
    }

    private void AddShot(Weapon weapon)
    {
        _statsKeeper.ShotsFired++;
    }

    private void TargetDestroyed(TargetManager targetManager, DamageReceiver target)
    {
        _statsKeeper.TargetsHit++;

        _scoreKeeper.Score += _targetPointValue;

        if (targetManager.TargetsDestroyedSinceWaveSpawn == targetManager.TargetsPerWave)
        {
            _scoreKeeper.Score += _wavePointValue;
        }
    }

    private void BotDestroyed(BotManager botManager, BotBrain brain, DamageReceiver dr)
    {
        _scoreKeeper.Score += (int)brain.SpawnedPointValue;

        _statsKeeper.TargetsHit++;
    }
}