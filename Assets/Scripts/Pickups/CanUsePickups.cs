using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanUsePickups : MonoBehaviour
{
    [SerializeField]
    private WeaponHolder _weaponHolder;
    public WeaponHolder WeaponHolder => _weaponHolder;
}
