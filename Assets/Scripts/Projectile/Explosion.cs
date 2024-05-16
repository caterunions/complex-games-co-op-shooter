using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Explosion
{
    public static void SpawnExplosion(Vector3 location, float damage, float radius, GameObject spawner)
    {
        Collider[] hits = Physics.OverlapSphere(location, radius);
        foreach(Collider hit in hits)
        {
            DamageReceiver dr = hit.GetComponentInParent<DamageReceiver>();
            if(dr != null)
            {
                Vector3 damagePos = hit.ClosestPoint(location);
                float distance = Mathf.InverseLerp(0, radius, (damagePos - location).magnitude);
                float adjustedDamage = Mathf.Lerp(damage, damage / 2, distance * distance);

                dr.ReceiveDamage(new(adjustedDamage, spawner));
            }
        }
    }
}
