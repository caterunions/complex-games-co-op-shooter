using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetManager : EntityManager
{
    public event Action<TargetManager, DamageReceiver, Transform> OnSpawnTarget;
    public event Action<TargetManager, DamageReceiver> OnTargetDestroy;

    [SerializeField]
    private List<Transform> _targetSpawnPoints;
    private List<Transform> _availableSpawns;

    [Range(1, 10)]
    public int TargetsPerWave = 5;
    public int TargetsDestroyedSinceWaveSpawn { get; private set; } = 0;

    private List<DamageReceiver> _activeTargets = new List<DamageReceiver>();

    [SerializeField]
    private DamageReceiver _target;

    private void OnEnable()
    {
        TargetsDestroyedSinceWaveSpawn = 0;
        _availableSpawns = new List<Transform>(_targetSpawnPoints);
        SpawnTargets(TargetsPerWave);
    }

    private void OnDisable()
    {
        CleanUpEntities();
    }

    public override void CleanUpEntities()
    {
        foreach(DamageReceiver target in _activeTargets)
        {
            Destroy(target.gameObject);
        }
        _activeTargets.Clear();
        _availableSpawns = new List<Transform>(_targetSpawnPoints);
    }

    private void OnTargetDamage(DamageReceiver target, DamageEvent dmgEvent, DamageResult result)
    {
        if (!_activeTargets.Contains(target)) return;

        if(result.Killed)
        {
            _activeTargets.Remove(target);
            TargetsDestroyedSinceWaveSpawn++;

            OnTargetDestroy?.Invoke(this, target);
            Destroy(target.gameObject, 0.1f);

            if (TargetsDestroyedSinceWaveSpawn >= TargetsPerWave)
            {
                _availableSpawns = new List<Transform>(_targetSpawnPoints);
                _availableSpawns.Remove(target.transform.parent);
                TargetsDestroyedSinceWaveSpawn = 0;
                SpawnTargets(TargetsPerWave);
            }
        }
    }

    public bool SpawnTargets(int count)
    {
        if(_availableSpawns.Count >= count)
        {
            for (int i = 0; i < count; i++)
            {
                int chosenSpawn = Random.Range(0, _availableSpawns.Count - 1);
                DamageReceiver target = Instantiate(_target.gameObject, _availableSpawns[chosenSpawn].position, _availableSpawns[chosenSpawn].rotation, _availableSpawns[chosenSpawn]).GetComponent<DamageReceiver>();
                target.OnDamage += OnTargetDamage;

                OnSpawnTarget?.Invoke(this, target, _availableSpawns[chosenSpawn]);
                _activeTargets.Add(target);
                _availableSpawns.RemoveAt(chosenSpawn);
            }
            return true;
        }
        else
        {
            Debug.LogWarning("TargetManager was told to spawn more targets than it could");
            return false;
        }
    }
}