using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAudioClip : AudioPlayer
{
    public override void Play()
    {
        AudioSource source = base._source;

        source.PlayOneShot(_clips[Random.Range(0, _clips.Count)]);
    }
}
