using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class SpawnExplosionsOnImpact : MonoBehaviour
{
    [SerializeField]
    private float _radius;

    private Projectile _projectile;
    private Projectile Projectile
    {
        get
        {
            if(_projectile == null) _projectile = GetComponent<Projectile>();
            return _projectile;
        }
    }

    private void OnEnable()
    {
        Projectile.OnHit += SpawnExplosion;
    }

    private void OnDisable()
    {
        Projectile.OnHit -= SpawnExplosion;
    }

    private void SpawnExplosion(Projectile projectile, Collision coll)
    {
        Vector3 spawn = coll.GetContact(0).point;

        Explosion.SpawnExplosion(spawn, projectile.Damage, _radius, projectile.gameObject);
    }
}
