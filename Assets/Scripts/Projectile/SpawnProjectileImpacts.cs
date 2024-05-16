using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class SpawnProjectileImpacts : MonoBehaviour
{
    [SerializeField]
    private GameObject _impactFX;
    [SerializeField]
    private LayerMask _impactFXMask = ~0;
    [SerializeField]
    private float _impactFXCleanupTime = 1f;
    [SerializeField]
    private GameObject _decal;
    [SerializeField]
    private LayerMask _decalMask = ~0;
    [SerializeField]
    private float _decalCleanupTime = 3f;

    private Projectile _projectile;

    private void OnEnable()
    {
        if(_projectile == null) _projectile = GetComponent<Projectile>();

        _projectile.OnHit += SpawnImpact;
    }

    private void OnDisable()
    {
        _projectile.OnHit -= SpawnImpact;
    }

    private void SpawnImpact(Projectile proj, Collision coll)
    {
        ContactPoint hit = coll.GetContact(0);
        int layer = coll.gameObject.layer;

        if (_impactFX != null && _impactFXMask == (_impactFXMask | (1 << layer)))
        {
            GameObject fx = Instantiate(_impactFX, hit.point, Quaternion.identity);
            Destroy(fx, _impactFXCleanupTime);
        }
        if (_decal != null && _decalMask == (_decalMask | (1 << layer)))
        {
            GameObject decal = Instantiate(_decal, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal), coll.transform);
            Destroy(decal, _decalCleanupTime);
        }
    }
}