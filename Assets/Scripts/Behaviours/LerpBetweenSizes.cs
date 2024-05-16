using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpBetweenSizes : MonoBehaviour
{
    public event Action<LerpBetweenSizes> OnComplete;

    [SerializeField]
    private Transform _transformToLerp;
    public Transform TransformToLerp 
    {
        get { return _transformToLerp;}
        set { _transformToLerp = value; }
    }

    [SerializeField]
    bool _executeOnEnable = false;
    [SerializeField]
    private float _targetSize = 1.0f;
    [SerializeField]
    private float _time = 1f;

    private void OnEnable()
    {
        if (TransformToLerp == null) TransformToLerp = transform;

        if(_executeOnEnable)
        {
            StartLerp(TransformToLerp.localScale.x, _targetSize, _time);
        }
    }

    public void StartLerp(float startScale, float endScale, float seconds)
    {
        StartLerp(startScale, endScale, seconds, null);
    }

    public void StartLerp(float startScale, float endScale, float seconds, Action onComplete)
    {
        StartCoroutine(LerpToScaleInSeconds(startScale, endScale, seconds, onComplete));
    }

    private IEnumerator LerpToScaleInSeconds(float startScale, float endScale, float seconds, Action onComplete)
    {
        Vector3 _startSize = Vector3.one * startScale;
        TransformToLerp.localScale = _startSize;
        Vector3 _endSize = Vector3.one * endScale;

        float startTime = Time.time;
        float endTime = startTime + seconds;

        while (Time.time < endTime)
        {
            TransformToLerp.localScale = Vector3.Lerp(_startSize, _endSize, Mathf.InverseLerp(startTime, endTime, Time.time));
            yield return null;
        }
        TransformToLerp.localScale = _endSize;

        if(onComplete != null) onComplete();
        OnComplete?.Invoke(this);
    }
}
