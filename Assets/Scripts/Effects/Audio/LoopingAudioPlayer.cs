using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingAudioPlayer : AudioPlayer
{
    private new void OnEnable()
    {
        _source.clip = _clips[0];
    }

    public override void Play()
    {
        _source.Play();
    }

    public void Pause()
    {
        _source.Pause();
    }
}
