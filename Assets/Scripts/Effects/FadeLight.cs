using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FadeLight : MonoBehaviour
{
    [SerializeField]
    private float _startIntensity;
    [SerializeField]
    private float _endIntensity;
    [SerializeField]
    private float _time;

    private Light _light;

    private void OnEnable()
    {
        if(_light == null) _light = GetComponent<Light>();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        _light.intensity = _startIntensity;

        float startTime = Time.time;
        float endTime = startTime + _time;

        while (Time.time < endTime)
        {
            _light.intensity = Mathf.Lerp(_startIntensity, _endIntensity, Mathf.InverseLerp(startTime, endTime, Time.time));
            yield return null;
        }

        _light.intensity = _endIntensity;
    }
}
