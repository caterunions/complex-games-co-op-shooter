using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Again, I think the brain is acting as a holder of data rather than a facilitator. Aim wants to know the root transform, it also wants the weapon provider, which the brain can provide, but it doesn't actually need the brain itself,
//because it never asks the brain to do anything, it only asks the brain for things.
[RequireComponent(typeof(BotBrain))]
public class BotAim : MonoBehaviour
{
    [SerializeField]
    private BotStats _stats;
    [SerializeField]
    private Transform _mainTransform;
    [SerializeField]
    private WeaponHolder _weaponHolder;

    private bool _withinFireAngle;
    public bool WithinFireAngle => _withinFireAngle;

    private AITargetable _target;
    public bool CanRotate { get; set; } = true;

    public void SetTarget(AITargetable target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null) return;

        Vector3 targetPos = _target.TargetPoint.position;
        float step = _stats.RotationSpeed * Time.deltaTime;

        Quaternion mainTargetRot = Quaternion.Euler(0f, Quaternion.LookRotation(targetPos - _mainTransform.position).eulerAngles.y, 0f);
        if(CanRotate) _mainTransform.rotation = Quaternion.RotateTowards(_mainTransform.rotation, mainTargetRot, step);

        //do weapon based aiming if a weapon is detected
        if(_weaponHolder != null && _weaponHolder.CurrentWeapon != null) 
        {
            Transform gunAim = _weaponHolder.CurrentWeapon.transform;
            // check if target is within max fire angle + max range
            if (Quaternion.Angle(_mainTransform.rotation, mainTargetRot) <= _stats.MaxFireAngle && Vector3.Distance(_mainTransform.position, targetPos) <= _stats.FireDistance)
            {
                _withinFireAngle = true;
                gunAim.rotation = Quaternion.RotateTowards(gunAim.rotation, Quaternion.LookRotation(targetPos - gunAim.position), step);
            }
            else _withinFireAngle = false;
        }
    }
}
