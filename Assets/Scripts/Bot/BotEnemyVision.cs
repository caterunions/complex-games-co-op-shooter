using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This feels like it's maybe bot sight, or bot fire should just take responsibility for making the weapon fire as well
public class BotEnemyVision : MonoBehaviour
{
    public event Action<BotEnemyVision, bool> OnBotEnemySightChange;

    [SerializeField]
    private BotStats _stats;

    private bool _enemyInSight;
    public bool EnemyInSight
    {
        get { return _enemyInSight; }
        set
        {
            var oldValue = _enemyInSight;
            _enemyInSight = value;
            if (value != oldValue) OnBotEnemySightChange?.Invoke(this, oldValue);
        }
    }

    public WeaponHolder WeaponHolder { get; set; }
    public BotAim Aim { get; set; }

    private AITargetable _target;

    public void SetTarget(AITargetable target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_stats == null) return;
        if (Aim == null) return;
        if (WeaponHolder == null) return;

        EnemyInSight = WeaponHolder.CurrentWeapon.IsNotObstructed(_target, _stats.SightObstructionLayers) && Aim.WithinFireAngle;
    }
}
