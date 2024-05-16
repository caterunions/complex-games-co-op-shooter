using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundAxis : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 1.0f;
    [SerializeField]
    private Vector3 _axis;
    [SerializeField]
    private bool _random = false;

    private void OnEnable()
    {
        if (_random) _axis = new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
    }

    private void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(_rotationSpeed * 100f * Time.deltaTime, _axis);
    }
}
