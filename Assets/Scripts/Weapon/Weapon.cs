using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public interface IFireControl
{
    void Fire( Weapon weapon, Action<Quaternion> onFire, Action onComplete);
}

public static class WeaponExtensions
{
    public static bool IsNotObstructed(this Weapon weapon, AITargetable target, LayerMask obstructionLayers)
    {
        return !Physics.Raycast(weapon.Launcher.Muzzle.position, weapon.Launcher.Muzzle.forward, Vector3.Distance(weapon.Launcher.Muzzle.position, target.TargetPoint.position), obstructionLayers);
    }
}

public class Weapon : MonoBehaviour, IIKProvider
{
    private class DefaultFireControl : IFireControl
    {
        public void Fire(Weapon weapon, Action<Quaternion> onFire, Action onComplete )
        {
            onFire(Quaternion.identity);
            onComplete();
        }
    }

    private DefaultFireControl defaultFireControl = new();

    public event Action<Weapon, bool> OnTriggerStatusChange;
    public event Action<Weapon> OnFireComplete;

    [SerializeField]
    private Projectile _projectile;
    public Projectile Projectile => _projectile;

    [SerializeField]
    private float _damage;
    public float Damage => _damage;

    [SerializeField]
    private Transform _handIK;

    [SerializeField]
    private ProjectileLauncher _launcher;
    public ProjectileLauncher Launcher
    {
        get
        {
            if(_launcher == null) _launcher = GetComponent<ProjectileLauncher>();
            return _launcher;
        }
    }

    [SerializeField]
    private float _velocity;

    [SerializeField]
    private List<Restriction> _restrictions;

    public Rigidbody ReferenceFrame { get; set; }
    private Vector3 RelativeVelocity => ReferenceFrame == null ? Vector3.zero : ReferenceFrame.velocity;

    private IFireControl _fireControl;
    public IFireControl FireControl
    {
        protected get { if (_fireControl != null) return _fireControl; return defaultFireControl; }
        set { _fireControl = value; }
    }

    private bool _firing;
    public bool Firing
    {
        get
        {
            return _firing;
        }
        set
        {
            var oldValue = value;
            _firing = value;
            OnTriggerStatusChange?.Invoke(this, oldValue);
        }
    }

    public bool CanFire => _restrictions.All(r => r.CanFire());

    private void Update()
    {
        if(_firing && CanFire)
        {
            FireControl.Fire(this, Fire, () => OnFireComplete?.Invoke(this) );
        }
    }

    private void Fire(Quaternion rotateBy)
    {
        _launcher.HandleLaunch(_projectile, _velocity, rotateBy, _damage, RelativeVelocity);
    }

    public void ConfigureIK(Animator animator, AvatarIKGoal goal)
    {
        if(_handIK != null)
        {
            animator.SetIKPositionWeight(goal, 1);
            animator.SetIKRotationWeight(goal, 1);
            animator.SetIKPosition(goal, _handIK.position);
            animator.SetIKRotation(goal, _handIK.rotation);
        }
    }

    public Restriction HasRestriction<T>() where T : Restriction
    {
        return _restrictions.FirstOrDefault(r => r is T);
    }
}