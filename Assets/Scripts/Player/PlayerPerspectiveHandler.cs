using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Xml.Linq;
using Object = UnityEngine.Object;

public class PlayerPerspectiveHandler : MonoBehaviour
{
    // If possible, it would be ideal if rather than having one field for Object, it could have a GameObject and a Behaviour field, and a custom inspector is setup to only allow one to be set at a time.
    // Then there's no possibility of accidentally putting something in that would trigger the error condition, or have it figure out which one it should actually use.
    // I can consider that out of scope at this time however.
    [Serializable]
    public struct FPUIElement
    {
        [SerializeField]
        private Object _element;
        [SerializeField]
        private bool _activeFirstPerson;

        public void SetElementActive(bool firstPerson)
        {
            if (_element is GameObject go) go.SetActive(_activeFirstPerson == firstPerson);
            else if (_element is Behaviour bh) bh.enabled = _activeFirstPerson == firstPerson;
            else Debug.LogError("FPUIElement is neither GameObject nor Behaviour");
        }
    }

    [SerializeField]
    private List<FPUIElement> _firstPersonElements;

    [SerializeField]
    private CinemachineVirtualCamera _firstPersonVCam;
    [SerializeField]
    private CinemachineVirtualCamera _thirdPersonVCam;

    [SerializeField]
    private PlayerLook _playerLook;

    private bool _isFirstPerson = false;
    public bool IsFirstPerson
    {
        get { return _isFirstPerson; }
        set
        {
            _isFirstPerson = value;
            foreach (FPUIElement e in _firstPersonElements)
            {
                e.SetElementActive(IsFirstPerson);
            }
            if (IsFirstPerson)
            {
                _firstPersonVCam.Priority = 1000;
                _thirdPersonVCam.Priority = 0;
                _playerLook.MaxVerticalLook = 80f;
                _playerLook.MinVerticalLook = -80f;
            }
            else
            {
                _firstPersonVCam.Priority = 0;
                _thirdPersonVCam.Priority = 1000;
                _playerLook.MaxVerticalLook = 70f;
                _playerLook.MinVerticalLook = -55f;
                _playerLook.HandleLook(new Vector2(0, 0));
            }
        }
    }
}