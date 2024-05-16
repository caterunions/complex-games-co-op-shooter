using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotManager : EntityManager
{
    public event Action<BotManager, BotBrain, DamageReceiver> OnBotDestroy;

    [Serializable]
    public struct Bot
    {
        [SerializeField]
        private BotBrain _botBrain;
        public BotBrain BotBrain => _botBrain;

        [SerializeField]
        private float _pointCost;
        public float PointCost => _pointCost;
    }

    [SerializeField]
    private AITargetable _player;
    [SerializeField]
    private List<Bot> _spawnableBots;
    [SerializeField]
    private List<Transform> _spawnPoints;
    [SerializeField]
    private float _maxPoints;
    [SerializeField]
    private float _minSpawnDistanceFromPlayer = 5f;

    private float _pointsSpent = 0f;
    private int _queuedSpawnIndex = 0;
    List<DamageReceiver> _activeBots = new List<DamageReceiver>();

    private void Update()
    {
        if (_pointsSpent + _spawnableBots[_queuedSpawnIndex].PointCost <= _maxPoints) _queuedSpawnIndex = SpawnBot(_spawnableBots[_queuedSpawnIndex]);
    }

    private int SpawnBot(Bot bot)
    {
        List<Transform> validSpawns = _spawnPoints.Where(t => Vector3.Distance(t.position, _player.TargetPoint.position) >= _minSpawnDistanceFromPlayer).ToList();

        if (validSpawns.Count != 0)
        {
            Vector3 chosenSpawnPoint = validSpawns[Random.Range(0, validSpawns.Count)].position;

            _pointsSpent += bot.PointCost;

            BotBrain spawnedBot = Instantiate(bot.BotBrain, chosenSpawnPoint, Quaternion.identity);
            spawnedBot.Target = _player;
            spawnedBot.DamageReceiver.OnDeath += OnBotDeath;
            spawnedBot.SpawnedPointValue = bot.PointCost;
            _activeBots.Add(spawnedBot.DamageReceiver);
        }

        return Random.Range(0, _spawnableBots.Count);
    }

    private void OnDisable()
    {
        CleanUpEntities();
    }

    private void OnBotDeath(DamageReceiver bot, DamageEvent dmgEvent, DamageResult result)
    {
        if (!_activeBots.Contains(bot)) return;
        BotBrain brain = bot.GetComponent<BotBrain>();
        if (brain == null) return;

        _activeBots.Remove(bot);

        _pointsSpent -= brain.SpawnedPointValue;
        OnBotDestroy?.Invoke(this, brain, bot);

        Destroy(bot.gameObject);
    }

    public override void CleanUpEntities()
    {
        foreach(DamageReceiver bot in _activeBots)
        {
            if(bot != null) Destroy(bot.gameObject);
        }
        _activeBots.Clear();
        _pointsSpent = 0;
    }
}
