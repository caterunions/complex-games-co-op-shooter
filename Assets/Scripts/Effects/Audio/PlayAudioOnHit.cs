using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnHit : MonoBehaviour
{
    [SerializeField]
    private AudioPlayer _player;
    [SerializeField]
    private DamageReceiver _damageReceiver;
    [SerializeField]
    private bool _playOnDeath;

    private void OnEnable()
    {
        _damageReceiver.OnDamage += TryPlayAudio;
    }

    private void OnDisable()
    {
        _damageReceiver.OnDamage -= TryPlayAudio;
    }

    private void TryPlayAudio(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        if (_playOnDeath)
        {
            _player.Play();
        }
        else if(!result.Killed)
        {
            _player.Play();
        }
    }
}
