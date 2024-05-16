using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDangerAudio : MonoBehaviour
{
    [SerializeField]
    private TimeRemainingDisplay _timeDisplay;
    [SerializeField]
    private AudioPlayer _player;

    private void OnEnable()
    {
        _timeDisplay.OnDangerPulse += PlayDangerAudio;
    }

    private void OnDisable()
    {
        _timeDisplay.OnDangerPulse -= PlayDangerAudio;
    }

    private void PlayDangerAudio(TimeRemainingDisplay timeDisplay, TimeKeeper timeKeeper)
    {
        _player.Play();
    }
}
