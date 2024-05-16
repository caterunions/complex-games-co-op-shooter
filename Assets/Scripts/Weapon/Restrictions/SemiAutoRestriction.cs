using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class SemiAutoRestriction : Restriction
{
    private bool _hasFired = false;

    private void OnEnable()
    {
        Weapon.OnTriggerStatusChange += UpdateCanFire;
    }

    private void OnDisable()
    {
        Weapon.OnTriggerStatusChange -= UpdateCanFire;
    }

    public override bool CanFire()
    {
        if (_hasFired) return false;

        _hasFired = true;
        return true;
    }

    private void UpdateCanFire(Weapon weapon, bool firing)
    {
        if (!weapon.Firing) _hasFired = false;
    }
}
