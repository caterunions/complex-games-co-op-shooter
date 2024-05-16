using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownAudio : MonoBehaviour
{
    [SerializeField]
    private CountdownDisplay _countdown;
    [SerializeField]
    private AudioPlayer _countdownSound;
    [SerializeField]
    private AudioPlayer _startSound;

    private void OnEnable()
    {
        _countdown.OnCountdownTimeChange += PlaySoundOnCountdown;
        _countdown.TimeKeeper.OnTimeRunOut += PlayStartSound;
    }

    private void OnDisable()
    {
        _countdown.OnCountdownTimeChange -= PlaySoundOnCountdown;
        _countdown.TimeKeeper.OnTimeRunOut -= PlayStartSound;
    }

    private void PlaySoundOnCountdown(CountdownDisplay countdown, int prev)
    {
        _countdownSound.Play();
    }

    private void PlayStartSound(TimeKeeper timeKeeper)
    {
        _startSound.Play();
    }
}
