using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public event Action<ProjectileLauncher, Projectile> OnProjectileLaunch;

    [SerializeField]
    private Transform _muzzle;
    public Transform Muzzle => _muzzle;

    public void HandleLaunch(Projectile prefab, float muzzleVelocity, Quaternion shotDir, float damage = 0f, Vector3 relativeVelocity = default)
    {
        Projectile spawnedProj = Instantiate(prefab, _muzzle.position, _muzzle.rotation * shotDir);
        Rigidbody projRB = spawnedProj.GetComponent<Rigidbody>();
        projRB.velocity = (spawnedProj.transform.forward * muzzleVelocity) + (spawnedProj.transform.forward * relativeVelocity.magnitude);

        spawnedProj.Damage = damage;

        FinishSetup(spawnedProj);

        OnProjectileLaunch?.Invoke(this, spawnedProj);
    }


    protected virtual void FinishSetup(Projectile spawnedProj) { }
}