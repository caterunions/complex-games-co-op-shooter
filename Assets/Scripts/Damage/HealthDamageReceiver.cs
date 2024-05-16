using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

//Rather than have health pool need to be extended to support multiple pools, this could have a list of health pools where it applies damage in sequence and then checks if the first (or last) pool has exhausted.
//This way if you want to have another kind of defense, like maybe armor, they can all work independently, and you can even mix and match which units have which kinds of protection.
public class HealthDamageReceiver : DamageReceiver
{
    [SerializeField]
    private List<HealthPool> _healthPools;

    protected override DamageResult HandleDamage(DamageEvent evt)
    {
        if (_healthPools.Count == 0)
        {
            Debug.LogWarning("HealthDamageReceiver has no health pools to apply damage to.", this);
            return new DamageResult(0f, false);
        }

        float remainingDamage = evt.BaseAmount;
        while(remainingDamage > 0f)
        {
            HealthPool curPool = _healthPools.Where(hp => hp.HP > 0f).LastOrDefault();
            if(curPool == null) break;

            remainingDamage -= curPool.TakeDamage(remainingDamage);
        }

        return new DamageResult(evt.BaseAmount - remainingDamage, _healthPools[0].HP <= 0);
    }

    private void OnEnable()
    {
        _healthPools[0].OnHPChange += CheckForRevive;
    }

    private void OnDisable ()
    {
        _healthPools[0].OnHPChange -= CheckForRevive;
    }

    private void CheckForRevive(HealthPool healthPool, float prevHP)
    {
        if(prevHP <= 0f && healthPool.HP > 0f)
        {
            Damageable = true;
        }
    }
}