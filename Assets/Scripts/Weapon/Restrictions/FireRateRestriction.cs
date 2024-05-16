using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon)), RequireComponent(typeof(TimeKeeper))]
public class FireRateRestriction : Restriction
{
    [SerializeField]
    private float _fireRate = 1f;

    private TimeKeeper _timeKeeper;
    private TimeKeeper TimeKeeper
    {
        get
        {
            if(_timeKeeper == null) _timeKeeper = GetComponent<TimeKeeper>();

            return _timeKeeper;
        }
    }

    private void OnEnable()
    {
        Weapon.OnFireComplete += ResetFireRateTimer;
        TimeKeeper.SetupTimer(1 / _fireRate, float.MaxValue);
    }

    private void OnDisable()
    {
        Weapon.OnFireComplete -= ResetFireRateTimer;
    }

    public override bool CanFire()
    {
        return TimeKeeper.TimeRemaining <= 0f || TimeKeeper.TimeRemaining == float.MaxValue;
    }

    private void ResetFireRateTimer(Weapon weapon)
    {
        TimeKeeper.ResetTimer();
    }

    public void ApplyFireRateMultiplier(float mult)
    {
        _fireRate *= mult;
    }
}
