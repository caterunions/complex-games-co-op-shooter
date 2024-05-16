using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownDisplay : MonoBehaviour
{
    public event Action<CountdownDisplay, int> OnCountdownTimeChange;

    [SerializeField]
    private TextMeshProUGUI _countdownText;
    [SerializeField]
    private TimeKeeper _timeKeeper;
    public TimeKeeper TimeKeeper => _timeKeeper;

    private int _countdownTime;
    public int CountdownTime
    {
        get { return _countdownTime; }
        set
        {
            if (value != _countdownTime) OnCountdownTimeChange?.Invoke(this, _countdownTime);
            _countdownTime = value;
        }
    }

    void Update()
    {
        CountdownTime = Mathf.CeilToInt(_timeKeeper.TimeRemaining);
        _countdownText.text = CountdownTime.ToString("0");
    }
}
