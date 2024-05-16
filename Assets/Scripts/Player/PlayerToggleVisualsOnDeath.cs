using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToggleVisualsOnDeath : MonoBehaviour
{
    [SerializeField]
    private DamageReceiver _playerDamageReceiver;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private List<GameObject> _playerVisuals;

    private void OnEnable()
    {
        _playerDamageReceiver.OnDeath += DisablePlayerVisuals;
        _gameManager.OnGameStateChange += EnablePlayerVisuals;
    }

    private void OnDisable()
    {
        _playerDamageReceiver.OnDeath -= DisablePlayerVisuals;
        _gameManager.OnGameStateChange -= EnablePlayerVisuals;
    }

    private void TogglePlayerVisuals(bool visible)
    {
        foreach(GameObject obj in _playerVisuals)
        {
            obj.SetActive(visible);
        }
    }

    private void DisablePlayerVisuals(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        TogglePlayerVisuals(false);
    }

    private void EnablePlayerVisuals(GameManager manager, GameState state)
    {
        if(state == GameState.Gameplay) TogglePlayerVisuals(true);
    }
}
