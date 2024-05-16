using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponPickup : MonoBehaviour
{
    [SerializeField]
    private Weapon _weapon;

    private void OnTriggerEnter(Collider other)
    {
        CanUsePickups canUsePickups = other.gameObject.GetComponentInParent<CanUsePickups>();
        if (canUsePickups != null)
        {
            canUsePickups.WeaponHolder.SwitchWeapon(_weapon, true);
        }
    }
}
