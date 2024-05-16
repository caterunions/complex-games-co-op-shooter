using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class ParticleEmissionOnWeaponFiring : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particles;

    private Weapon _weapon;
    private Weapon Weapon
    {
        get
        {
            if( _weapon == null ) _weapon = GetComponent<Weapon>();
            return _weapon;
        }
    }

    private void OnEnable()
    {
        ToggleParticles(Weapon, Weapon.Firing);
        Weapon.OnTriggerStatusChange += ToggleParticles;
    }

    private void OnDisable()
    {
        Weapon.OnTriggerStatusChange -= ToggleParticles;
    }

    private void ToggleParticles(Weapon weapon, bool firing)
    {
        if (firing) _particles.Play();
        else _particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}
