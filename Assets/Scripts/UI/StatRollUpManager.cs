using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatRollUpManager : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private ScoreKeeper _scoreKeeper;
    [SerializeField]
    private StatsKeeper _statsKeeper;

    [SerializeField]
    private RollTextUp _scoreRollUp;
    [SerializeField]
    private RollTextUp _shotsRollUp;
    [SerializeField]
    private RollTextUp _targetsRollUp;
    [SerializeField]
    private RollTextUp _accuracyRollUp;

    private void OnEnable()
    {
        _gameManager.OnGameStateChange += TryUpdateFields;
    }

    private void OnDisable()
    {
        _gameManager.OnGameStateChange -= TryUpdateFields;
    }

    private void TryUpdateFields(GameManager gameManager, GameState state)
    {
        if(state == GameState.StatsDisplay)
        {
            _scoreRollUp.EndValue = _scoreKeeper.Score;
            _shotsRollUp.EndValue = _statsKeeper.ShotsFired;
            _targetsRollUp.EndValue = _statsKeeper.TargetsHit;
            _accuracyRollUp.EndValue = _statsKeeper.Accuracy;
        }
    }
}
