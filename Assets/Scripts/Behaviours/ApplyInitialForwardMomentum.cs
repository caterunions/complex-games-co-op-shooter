using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyInitialForwardMomentum : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    [SerializeField]
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        if(_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.velocity = transform.forward * Speed;
    }
}
