using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class GenerateCinemachineImpulseOnEnable : MonoBehaviour
{
    private CinemachineImpulseSource _source;
    private void OnEnable()
    {
        if(_source == null) _source = GetComponent<CinemachineImpulseSource>();
        _source.GenerateImpulse();
    }
}
