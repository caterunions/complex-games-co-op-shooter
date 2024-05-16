using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(Weapon))]
public class LoopingAudioOnWeaponFire : MonoBehaviour
{
    [SerializeField]
    private LoopingAudioPlayer _player;

    private Weapon _weapon;
    private Weapon Weapon
    {
        get
        {
            if (_weapon == null) _weapon = GetComponent<Weapon>();
            return _weapon;
        }
    }

    private void OnEnable()
    {
        ToggleAudio(Weapon, Weapon.Firing);
        Weapon.OnTriggerStatusChange += ToggleAudio;
    }

    private void OnDisable()
    {
        Weapon.OnTriggerStatusChange -= ToggleAudio;
    }

    private void ToggleAudio(Weapon weapon, bool firing)
    {
        if (firing) _player.Play();
        else _player.Pause();
    }
}
