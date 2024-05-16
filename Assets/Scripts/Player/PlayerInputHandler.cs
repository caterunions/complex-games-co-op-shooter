using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField, Range(0.2f, 1f)]
    private float _sensitivity = 0.5f;

    [Header("Controller variables")]
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private PlayerInput _playerInput;
    [SerializeField]
    private PlayerLook _playerLook;
    [SerializeField]
    //private PlayerFire _playerFire;
    private WeaponHolder _weaponHolder;
    [SerializeField]
    private PlayerMove _playerMove;
    [SerializeField]
    private PlayerPerspectiveHandler _playerPerspectiveHandler;
    [SerializeField]
    private Animator _animator;

    private void OnEnable()
    {
        _gameManager.OnGameStateChange += ToggleInputOnStateChange;
        _playerInput.actions.FindAction("Look").performed += OnLook;
        _playerInput.actions.FindAction("Fire").performed += OnStartFire;
        _playerInput.actions.FindAction("Fire").canceled += OnStopFire;
        _playerInput.actions.FindAction("ChangeCamera").performed += OnChangeCamera;
        _playerInput.actions.FindAction("Move").performed += OnMoveStart;
        _playerInput.actions.FindAction("Move").canceled += OnMoveEnd;
    }

    private void OnDisable()
    {
        _gameManager.OnGameStateChange -= ToggleInputOnStateChange;
        _playerInput.actions.FindAction("Look").performed -= OnLook;
        _playerInput.actions.FindAction("Fire").performed -= OnStartFire;
        _playerInput.actions.FindAction("Fire").canceled -= OnStopFire;
        _playerInput.actions.FindAction("ChangeCamera").performed -= OnChangeCamera;
        _playerInput.actions.FindAction("Move").performed -= OnMoveStart;
        _playerInput.actions.FindAction("Move").canceled -= OnMoveEnd;
    }

    private void ToggleInputOnStateChange(GameManager gameManager, GameState state)
    {
        switch (state)
        {
            case GameState.Gameplay:
                EnableGameplayElements();
                break;
            default:
                DisableGameplayElements();
                break;
        }
    }

    private void OnLook(InputAction.CallbackContext ctx)
    {
        Vector2 lookDelta = ctx.ReadValue<Vector2>() * _sensitivity;
        _playerLook.HandleLook(lookDelta);
    }

    private void OnStartFire(InputAction.CallbackContext ctx)
    {
        _weaponHolder.CurrentWeapon.Firing = true;
    }

    private void OnStopFire(InputAction.CallbackContext ctx)
    {
        _weaponHolder.CurrentWeapon.Firing = false;
    }

    private void OnChangeCamera(InputAction.CallbackContext ctx)
    {
        _playerPerspectiveHandler.IsFirstPerson = !_playerPerspectiveHandler.IsFirstPerson;
    }

    private void OnMoveStart(InputAction.CallbackContext ctx)
    {
        _playerMove.HandleMove(ctx.ReadValue<Vector2>());
        _animator.SetBool("Moving", true);
    }

    private void OnMoveEnd(InputAction.CallbackContext ctx)
    {
        _playerMove.StopMove();
        _animator.SetBool("Moving", false);
    }

    private void EnableGameplayElements()
    {
        _playerLook.enabled = true;
        _playerInput.SwitchCurrentActionMap("Player");
        _playerPerspectiveHandler.IsFirstPerson = false;
        _playerMove.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisableGameplayElements()
    {
        _playerLook.enabled = false;
        _playerInput.SwitchCurrentActionMap("None");
        _playerMove.StopMove();
        _animator.SetBool("Moving", false);
        _playerMove.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
