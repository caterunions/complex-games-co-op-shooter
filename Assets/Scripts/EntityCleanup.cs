using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityCleanup : MonoBehaviour
{
    [SerializeField]
    private List<EntityManager> _entityManagers;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private List<GameState> _cleanupStates = new List<GameState>();

    private void OnEnable()
    {
        _gameManager.OnGameStateChange += TryCleanUpTargets;
    }

    private void OnDisable()
    {
        _gameManager.OnGameStateChange += TryCleanUpTargets;
    }

    private void TryCleanUpTargets(GameManager gameManager, GameState state)
    {
        if(_cleanupStates.Contains(state))
        {
            foreach(EntityManager manager in _entityManagers)
            {
                manager.CleanUpEntities();
            }
        }
    }
}
