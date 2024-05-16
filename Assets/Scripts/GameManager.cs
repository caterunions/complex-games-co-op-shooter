using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        // load title elements
        OnGameStateChange?.Invoke(this, GameState);
    }

    [SerializeField]
    private GameState _gameState = GameState.Title;
    public GameState GameState {
        get => _gameState;
        private set
        {
            _gameState = value;
            OnGameStateChange?.Invoke(this, _gameState);
        }
    }

    public event Action<GameManager, GameState> OnGameStateChange;

    [SerializeField]
    private TargetManager _targetManager;
    [SerializeField]
    private TimeKeeper _countdown;
    [SerializeField]
    private TimeKeeper _timeKeeper;
    [SerializeField]
    private StatsReveal _statsReveal;
    [SerializeField]
    private UIInputHandler _uiInputHandler;
    [SerializeField]
    private DamageReceiver _playerDamageReceiver;

    private void OnEnable()
    {
        _countdown.OnTimeRunOut += ChangeStateOnCountdown;
        _timeKeeper.OnTimeRunOut += ChangeStateOnTimerEnd;
        _playerDamageReceiver.OnDeath += ChangeStateOnPlayerDeath;
        _statsReveal.OnStatsFinishedDisplaying += ChangeStateOnStatDisplayEnd;
        _uiInputHandler.OnResetGame += ChangeStateOnGameLoop;
    }

    private void OnDisable()
    {
        _countdown.OnTimeRunOut -= ChangeStateOnCountdown;
        _timeKeeper.OnTimeRunOut -= ChangeStateOnTimerEnd;
        _playerDamageReceiver.OnDeath -= ChangeStateOnPlayerDeath;
        _statsReveal.OnStatsFinishedDisplaying -= ChangeStateOnStatDisplayEnd;
        _uiInputHandler.OnResetGame -= ChangeStateOnGameLoop;
    }

    private void ChangeStateOnCountdown(TimeKeeper timeKeeper)
    {
        GameState = GameState.Gameplay;
    }

    private void ChangeStateOnTimerEnd(TimeKeeper timeKeeper)
    {
        GameState = GameState.StatsDisplay;
    }

    private void ChangeStateOnPlayerDeath(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        GameState = GameState.StatsDisplay;
    }

    private void ChangeStateOnStatDisplayEnd(StatsReveal statsReveal)
    {
        GameState = GameState.PostGame;
    }

    private void ChangeStateOnGameLoop(UIInputHandler uiInputHandler)
    {
        GameState = GameState.PreGame;
    }
}

public enum GameState 
{
    Title,
    PreGame,
    Gameplay,
    StatsDisplay,
    PostGame
}