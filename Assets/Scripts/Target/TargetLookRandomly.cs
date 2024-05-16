using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLookRandomly : MonoBehaviour
{
    // full rewrite required, doesn't work
    [SerializeField]
    private Transform _lookTransform;
    [SerializeField]
    private float _maxLookAngle = 25f;
    [SerializeField]
    private float _lookSpeed = 1;

    private Vector2 _curLookDir;

    private void OnEnable()
    {
        _curLookDir = ChooseNewLook();
        if (_lookTransform == null) _lookTransform = transform;
    }

    private void Update()
    {
        float biggestAngle = Mathf.Max(_lookTransform.localRotation.eulerAngles.x, _lookTransform.localRotation.eulerAngles.z);
        float smallestAngle = Mathf.Min(_lookTransform.localRotation.eulerAngles.x, _lookTransform.localRotation.eulerAngles.z);
        if (biggestAngle > _maxLookAngle || smallestAngle < -_maxLookAngle)
        {
            _lookTransform.localRotation = Quaternion.Euler(Mathf.Clamp(_lookTransform.localRotation.x, -_maxLookAngle, _maxLookAngle), 0f, Mathf.Clamp(_lookTransform.localRotation.z, -_maxLookAngle, _maxLookAngle));
            _curLookDir = ChooseNewLook();
        }

        _lookTransform.localRotation *= Quaternion.AngleAxis(_curLookDir.x, Vector3.right);
        _lookTransform.localRotation *= Quaternion.AngleAxis(_curLookDir.y, Vector3.forward);
    }

    private Vector2 ChooseNewLook()
    {
        float num = Random.Range(0f, 2f);
        Vector2 newDir = new Vector2(num - 1f, 2f - num);

        return newDir * _lookSpeed;
    }
}
