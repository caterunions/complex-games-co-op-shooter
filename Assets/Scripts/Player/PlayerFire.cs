using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This class seems to not actually do anything aside from create an extra level of abstraction between WeaponHolders and ProjectileLaunchers
//Consider that WeaponHandler could be connected to directly for registering with CurrentWeapon.OnWeaponFire
/*
public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private WeaponHolder _weaponHolder;

    public event Action<PlayerFire, WeaponHolder, Weapon, Projectile> OnPlayerFireAnyWeapon;

    private void OnEnable()
    {
        if(_weaponHolder.CurrentWeapon != null) _weaponHolder.CurrentWeapon.OnWeaponFire += CallOnPlayerFireEvent;
    }

    private void OnDisable()
    {
        if (_weaponHolder.CurrentWeapon != null) _weaponHolder.CurrentWeapon.OnWeaponFire -= CallOnPlayerFireEvent;
    }

    public void PressWeaponTrigger()
    {
        _weaponHolder.CurrentWeapon.Firing = true;
    }

    public void ReleaseWeaponTrigger()
    {
        _weaponHolder.CurrentWeapon.Firing = false;
    }

    private void CallOnPlayerFireEvent(Weapon weapon, ProjectileLauncher launcher, Projectile projectile, List<Restriction> restrictions)
    {
        OnPlayerFireAnyWeapon?.Invoke(this, _weaponHolder, weapon, projectile);
    }
}
*/