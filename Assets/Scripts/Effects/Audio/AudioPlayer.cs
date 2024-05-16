using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    protected List<AudioClip> _clips = new List<AudioClip>();
    [SerializeField]
    protected AudioSource _source;
    [SerializeField]
    private bool _playOnEnable = false;

    protected void OnEnable()
    {
        if (_source == null) _source = GetComponent<AudioSource>();
        if (_playOnEnable) Play();
    }

    public virtual void Play()
    {
        _source.PlayOneShot(_clips[0]);
    }
}
