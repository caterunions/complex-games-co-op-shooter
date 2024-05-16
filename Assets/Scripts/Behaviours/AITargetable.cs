using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AITargetableExtensions
{

    public static Ray ToRay( this Transform src )
    {
        return new Ray(src.position, src.forward);
    }
}

public class AITargetable : MonoBehaviour
{
    [SerializeField]
    private Transform _targetPoint;
    public Transform TargetPoint => _targetPoint;

    private void OnEnable()
    {
        if(_targetPoint == null) _targetPoint = transform;
    }
}

