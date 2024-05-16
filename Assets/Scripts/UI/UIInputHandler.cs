using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;
    [SerializeField]
    private GameManager _gameManager;

    public event Action<UIInputHandler> OnResetGame;

    private void OnEnable()
    {
        _playerInput.actions.FindAction("Click").performed += OnMenuClick;
    }

    private void OnDisable()
    {
        _playerInput.actions.FindAction("Click").performed -= OnMenuClick;
    }

    private void OnMenuClick(InputAction.CallbackContext ctx)
    {
        switch(_gameManager.GameState)
        {
            case GameState.Title:
            case GameState.PostGame:
                OnResetGame?.Invoke(this);
                break;
        }
    }
}
