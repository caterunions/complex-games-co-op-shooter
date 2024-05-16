using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMuzzleFX : MonoBehaviour
{
    [SerializeField]
    private ProjectileLauncher _launcher;
    [SerializeField]
    private GameObject _muzzleFX;
    [SerializeField]
    private float _cleanupTime = 3f;

    private void OnEnable()
    {
        _launcher.OnProjectileLaunch += PlayMuzzleFX;
    }

    private void OnDisable()
    {
        _launcher.OnProjectileLaunch -= PlayMuzzleFX;
    }

    private void PlayMuzzleFX(ProjectileLauncher launcher, Projectile proj)
    {
        GameObject fxObj = Instantiate(_muzzleFX, launcher.Muzzle.position, launcher.Muzzle.rotation);
        Destroy(fxObj, _cleanupTime);
    }
}
