using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ToggleElementsOnStateChange : MonoBehaviour
{
    [Serializable]
    public struct StateSpecificElement
    {
        [SerializeField]
        private Object _element;
        [SerializeField]
        private List<GameState> _changeStates;

        public void SetElementActive(GameState s)
        {
            if (_element is GameObject go) go.SetActive(_changeStates.Contains(s));
            else if (_element is MonoBehaviour mbh) mbh.enabled = _changeStates.Contains(s);
            else Debug.LogError("StateSpecificElement is neither GameObject nor MonoBehaviour");
        }
    }

    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private List<StateSpecificElement> _toggleElements;

    private void OnEnable()
    {
        _gameManager.OnGameStateChange += ToggleElements;
    }

    private void OnDisable()
    {
        _gameManager.OnGameStateChange -= ToggleElements;
    }

    private void ToggleElements(GameManager gm, GameState state)
    {
        foreach(StateSpecificElement e in _toggleElements)
        {
            e.SetElementActive(state);
        }
    }
}
