using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRestriction
{
    bool CanFire();
}

// workaround for unity not serializing interfaces
public abstract class Restriction : MonoBehaviour, IRestriction
{
    private Weapon _weapon;
    protected Weapon Weapon
    {
        get
        {
            if (_weapon == null) _weapon = GetComponent<Weapon>();

            return _weapon;
        }
    }

    public abstract bool CanFire();
}
