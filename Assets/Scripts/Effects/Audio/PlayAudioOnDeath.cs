using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnDeath : MonoBehaviour
{
    [SerializeField]
    private AudioPlayer _player;
    [SerializeField]
    private DamageReceiver _damageReceiver;

    private void OnEnable()
    {
        _damageReceiver.OnDeath += PlayAudio;
    }

    private void OnDisable()
    {
        _damageReceiver.OnDeath -= PlayAudio;
    }

    private void PlayAudio(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        _player.Play();
    }
}
