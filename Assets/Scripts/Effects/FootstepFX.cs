using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class FootstepFX : MonoBehaviour
{
    [SerializeField]
    private AudioPlayer _player;
    [SerializeField]
    private ParticleSystem _particles;
    [SerializeField]
    private Transform _leftFoot;
    [SerializeField]
    private Transform _rightFoot;

    // called by unity animation system
    public void LeftFootStepCallbackFromAnimator()
    {
        _player.Play();
        Instantiate(_particles, _leftFoot.position, Quaternion.identity);
    }
    public void RightFootStepCallbackFromAnimator()
    {
        _player.Play();
        Instantiate(_particles, _rightFoot.position, Quaternion.identity);
    }
}
