using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBrain : MonoBehaviour
{
    public event Action<BotBrain, AITargetable> OnTargetChange;

    private AITargetable _target;
    public AITargetable Target
    {
        get
        {
            return _target;
        }
        set
        {
            AITargetable previousTarget = _target;
            _target = value;

            if (_aim != null)
            {
                _aim.SetTarget(Target);
            }
            if (_vision != null)
            {
                _vision.SetTarget(Target);
                _vision.Aim = _aim;
                _vision.WeaponHolder = _weaponHolder;
            }
            if (_navigation != null)
            {
                _navigation.SetTarget(Target);
            }

            OnTargetChange?.Invoke(this, previousTarget);
        }
    }

    [SerializeField]
    private Transform _mainTransform;
    [SerializeField]
    private BotAim _aim;
    [SerializeField]
    private BotEnemyVision _vision;
    [SerializeField]
    private BotNavigation _navigation;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private bool _canMoveWhileShooting = true;

    public float SpawnedPointValue { get; set; }

    [SerializeField]
    private DamageReceiver _damageReceiver;
    public DamageReceiver DamageReceiver => _damageReceiver;

    [SerializeField]
    private WeaponHolder _weaponHolder;
    private Weapon _curWeapon => _weaponHolder.CurrentWeapon;

    private void OnEnable()
    {
        if (_mainTransform == null) _mainTransform = transform;



        _vision.OnBotEnemySightChange += ToggleFiring;
    }

    private void OnDisable()
    {
        _vision.OnBotEnemySightChange -= ToggleFiring;
    }

    private void ToggleFiring(BotEnemyVision fire, bool prev)
    {
        if (!_curWeapon.Firing && fire.EnemyInSight)
        {
            _curWeapon.Firing = true;
        }
        else if (_curWeapon.Firing && !fire.EnemyInSight)
        {
            _curWeapon.Firing = false;
        }
    }

    private void Update()
    {
        if(_navigation == null) return;

        if(!_canMoveWhileShooting) _navigation.CanMove = !_curWeapon.Firing;
        _aim.CanRotate = _navigation.RemainingDistance <= _navigation.OpimalDistance + 3f;

        if(_animator == null) return;

        _animator.SetBool("Moving", (_canMoveWhileShooting && _navigation.Moving) || (!_curWeapon.Firing && _navigation.Moving));
        _animator.speed = _navigation.MoveSpeedAnimationMultiplier;
    }
}
