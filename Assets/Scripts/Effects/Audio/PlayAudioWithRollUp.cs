using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioWithRollUp : MonoBehaviour
{
    [SerializeField]
    private RollTextUp _rollUp;
    [SerializeField]
    private AudioPlayer _player;

    private void OnEnable()
    {
        _rollUp.OnCurrentValueChange += PlaySoundOnRollUp;
    }

    private void OnDisable()
    {
        _rollUp.OnCurrentValueChange -= PlaySoundOnRollUp;
    }

    private void PlaySoundOnRollUp(RollTextUp rollUp, float prev)
    {
        _player.Play();
    }
}
