using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetModelsOnGameReload : MonoBehaviour
{
    [SerializeField]
    private List<Resettable> _resetModels;
    [SerializeField]
    private GameState _resetState;
    [SerializeField]
    private GameManager _gameManager;

    private void OnEnable()
    {
        _gameManager.OnGameStateChange += TryResetModels;
    }

    private void OnDisable()
    {
        _gameManager.OnGameStateChange -= TryResetModels;
    }

    private void TryResetModels(GameManager gameManager, GameState state)
    {
        if(state == _resetState)
        {
            foreach(Resettable model in _resetModels)
            {
                model.ResetModel();
            }
        }
    }
}
