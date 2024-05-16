using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransformPosition : MonoBehaviour
{
    [SerializeField]
    private Transform _copy;
    [Header("Dimensions to copy:")]
    [SerializeField]
    private bool _x = true;
    [SerializeField]
    private bool _y = true;
    [SerializeField]
    private bool _z = true;

    private Vector3 _orig;

    private void OnEnable()
    {
        _orig = transform.localPosition;
    }

    private void OnDisable()
    {
        transform.localPosition = _orig;
    }

    private void Update()
    {
        transform.position = new Vector3(
            _x ? _copy.position.x : transform.position.x,
            _y ? _copy.position.y : transform.position.y,
            _z ? _copy.position.z : transform.position.z
            );
    }
}
