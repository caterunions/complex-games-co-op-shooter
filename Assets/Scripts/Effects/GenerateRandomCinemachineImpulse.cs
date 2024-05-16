using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class GenerateRandomCinemachineImpulse : MonoBehaviour
{
    [Header("Random Velocity Limits")]
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMin;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float zMin;
    [SerializeField]
    private float zMax;

    private CinemachineImpulseSource _source;
    private void OnEnable()
    {
        if (_source == null) _source = GetComponent<CinemachineImpulseSource>();
        _source.GenerateImpulseWithVelocity(new Vector3(
            Random.Range(xMin, xMax),
            Random.Range(yMin, yMax),
            Random.Range(zMin, zMax)));
    }
}
