using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUIElement : MonoBehaviour
{
    [SerializeField]
    private RectTransform _uiElement;
    [SerializeField]
    private bool _loop = false;
    [SerializeField]
    private Vector2 _startPos = Vector2.zero;
    [SerializeField]
    private Vector2 _endPos = Vector2.zero;
    [SerializeField]
    private float _time = 1f;

    private void OnEnable()
    {
        StartCoroutine(Scroll());
    }

    private void OnDisable()
    {
        StopCoroutine(Scroll());
    }

    private IEnumerator Scroll()
    {
        do
        {
            _uiElement.anchoredPosition = _startPos;

            float startTime = Time.time;
            float endTime = startTime + _time;

            while (Time.time < endTime)
            {
                _uiElement.anchoredPosition = Vector3.Lerp(_startPos, _endPos, Mathf.InverseLerp(startTime, endTime, Time.time));
                yield return null;
            }
            _uiElement.anchoredPosition = _endPos;
        } while (_loop);
    }
}
