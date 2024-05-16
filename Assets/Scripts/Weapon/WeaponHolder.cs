using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is cleraly a place holder, but that's ok for now, since WeaponChanging isn't an option yet.
public class WeaponHolder : Resettable
{
    public event Action<WeaponHolder, Weapon> OnWeaponChanged;

    [SerializeField]
    private Weapon _defaultWeapon;

    [SerializeField]
    private Weapon _currentWeapon;
    public Weapon CurrentWeapon => _currentWeapon;

    [SerializeField]
    private AudioPlayer _playAudioOnWeaponSwitch;
    [SerializeField]
    private Rigidbody _relativeVelocity;
    [SerializeField]
    private WeaponIKControl _weaponIKControl;

    private void OnEnable()
    {
        if (_defaultWeapon != null) SwitchWeapon(_defaultWeapon);
        if (_relativeVelocity != null) _currentWeapon.ReferenceFrame = _relativeVelocity;
        if (_weaponIKControl != null) _weaponIKControl.IKProvider = _currentWeapon;
    }

    public void SwitchWeapon(Weapon weapon, bool playAudio = false)
    {
        Weapon oldWeapon = _currentWeapon;
        _currentWeapon = Instantiate(weapon, transform);
        _weaponIKControl.IKProvider = _currentWeapon;

        if(playAudio && _playAudioOnWeaponSwitch != null) _playAudioOnWeaponSwitch.Play();

        OnWeaponChanged?.Invoke(this, oldWeapon);

        Destroy(oldWeapon.gameObject);
    }

    public override void ResetModel()
    {
        SwitchWeapon(_defaultWeapon);
    }
}
