using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeRemainingDisplay : MonoBehaviour
{
    public event Action<TimeRemainingDisplay, TimeKeeper> OnDangerPulse;

    [Header("References")]
    [SerializeField]
    private RectTransform _timerHand;
    [SerializeField] 
    private List<Image> _timerImages;
    [SerializeField]
    private TimeKeeper _timeKeeper;
    [SerializeField]
    private PlayerTrackTimeBonuses _timeBonusValues;

    [Header("Animation Variables")]
    [SerializeField]
    private float _normalBonusPulseDuration = 0.2f;
    [SerializeField]
    private float _normalBonusPulseSizeMult = 1.2f;
    [SerializeField]
    private Color _normalBonusPulseColor = new Color(0.8f, 1f, 0.8f, 1f);
    [SerializeField]
    private float _waveBonusPulseDuration = 0.2f;
    [SerializeField]
    private float _waveBonusPulseSizeMult = 1.2f;
    [SerializeField]
    private Color _waveBonusPulseColor = new Color(0.8f, 1f, 1f, 1f);
    [SerializeField]
    private float _dangerPulseDuration = 0.2f;
    [SerializeField]
    private float _dangerPulseSizeMult = 1.2f;
    [SerializeField]
    private Color _dangerPulseColor = new Color(1f, 0.8f, 0.8f, 1f);

    private Coroutine _curPulse;

    private Coroutine _dangerCoroutine;

    private void OnEnable()
    {
        foreach (Image img in _timerImages)
        {
            img.color = Color.white;
        }
        _timeKeeper.OnTimeAdded += OnTimeAdded;
    }

    private void OnDisable()
    {
        _timeKeeper.OnTimeAdded -= OnTimeAdded;
    }

    private void Update()
    {
        float handRot = 360f - _timeKeeper.TimeRemaining * 6f;
        _timerHand.rotation = Quaternion.AngleAxis(handRot, Vector3.forward);

        if(_timeKeeper.TimeRemaining <= _timeKeeper.MaxTime / 4 && _dangerCoroutine == null)
        {
            _dangerCoroutine = StartCoroutine(DangerRoutine());
        }
        else if (_timeKeeper.TimeRemaining >= _timeKeeper.MaxTime / 4 && _dangerCoroutine != null)
        {
            StopCoroutine(_dangerCoroutine);
            _dangerCoroutine = null;
        }
    }

    private IEnumerator DangerRoutine()
    {
        float wait = 2f;

        while(_timeKeeper.TimeRemaining <= _timeKeeper.MaxTime / 4)
        {
            Pulse(_dangerPulseColor, _dangerPulseSizeMult, _dangerPulseDuration);
            OnDangerPulse?.Invoke(this, _timeKeeper);

            if (_timeKeeper.TimeRemaining <= _timeKeeper.MaxTime / 16) wait = 0.25f;
            else if (_timeKeeper.TimeRemaining <= _timeKeeper.MaxTime / 8) wait = 0.5f;
            else if (_timeKeeper.TimeRemaining <= 3 * _timeKeeper.MaxTime / 16) wait = 1f;
            else wait = 2f;

            yield return new WaitForSeconds(wait);
        }
    }

    private void OnTimeAdded(TimeKeeper timeKeeper, float timeAdded)
    {
        if(timeAdded == _timeBonusValues.TargetTimeValue)
        {
            Pulse(_normalBonusPulseColor, _normalBonusPulseSizeMult, _normalBonusPulseDuration);
        }
        else if (timeAdded == _timeBonusValues.WaveTimeValue)
        {
            Pulse(_waveBonusPulseColor, _waveBonusPulseSizeMult, _waveBonusPulseDuration);
        }
    }

    private void Pulse(Color color, float size, float duration)
    {
        if(_curPulse == null)
        {
            _curPulse = StartCoroutine(PulseAnim(color, size, duration));
        } else
        {
            StopCoroutine(_curPulse);
            _curPulse = StartCoroutine(PulseAnim(color, size, duration));
        }
    }

    private IEnumerator PulseAnim(Color color, float size, float duration)
    {
        foreach (Image img in _timerImages)
        {
            img.color = color;
        }
        yield return null;

        float startTime = Time.time;
        float inEnd = startTime + (duration / 2);
        float outEnd = startTime + duration;

        // in
        while (Time.time < inEnd)
        {
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * size, Mathf.InverseLerp(startTime, inEnd, Time.time));
            yield return null;
        }

        // out
        while (Time.time < outEnd)
        {
            transform.localScale = Vector3.Lerp(Vector3.one * size, Vector3.one, Mathf.InverseLerp(inEnd, outEnd, Time.time));
            yield return null;
        }

        // reset
        foreach (Image img in _timerImages)
        {
            img.color = Color.white;
        }
    }
}