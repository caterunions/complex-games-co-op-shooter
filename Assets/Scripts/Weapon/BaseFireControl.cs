using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFireControl : MonoBehaviour, IFireControl
{
    [SerializeField]
    private Weapon weapon;
    protected Weapon Weapon => weapon;

    private void OnEnable()
    {
        if(weapon == null) weapon = GetComponent<Weapon>();

        weapon.FireControl = this;
    }

    private void OnDisable()
    {
        weapon.FireControl = null;
    }

    public abstract void Fire(Weapon weapon, Action<Quaternion> onFire, Action onComplete);
}