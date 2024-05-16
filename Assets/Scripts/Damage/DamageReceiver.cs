using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageResult
{
    public float DamageTaken { get; private set; }
    public bool Killed { get; private set; }

    public DamageResult(float damageTaken, bool killed)
    {
        DamageTaken = damageTaken;
        Killed = killed;
    }
}
public class DamageEvent
{
    public float BaseAmount { get; private set; }
    public GameObject Source { get; private set; }
#nullable enable
    public Collision? Collision { get; private set; }
#nullable disable

    public DamageEvent(float baseAmount, GameObject source)
    {
        BaseAmount = baseAmount;
        Source = source;
    }

    public DamageEvent(float baseAmount, GameObject source, Collision collision)
    {
        BaseAmount = baseAmount;
        Source = source;
        Collision = collision;
    }
}

public class DamageReceiver : MonoBehaviour
{
    public event Action<DamageReceiver, DamageEvent, DamageResult> OnDamage;
    public event Action<DamageReceiver, DamageEvent, DamageResult> OnDeath;

    public bool Damageable { get; set; } = true;

    public void ReceiveDamage(DamageEvent dmgEvent)
    {
        if (!Damageable) return;

        DamageResult result = HandleDamage(dmgEvent);

        OnDamage?.Invoke(this, dmgEvent, result);

        // don't allow ondamage to be called after a damagereceiver dies
        if(result.Killed)
        {
            OnDeath?.Invoke(this, dmgEvent, result);
            Damageable = false;
        }
    }

    protected virtual DamageResult HandleDamage(DamageEvent dmgEvent)
    {
        return new DamageResult(dmgEvent.BaseAmount, true);
    }
}