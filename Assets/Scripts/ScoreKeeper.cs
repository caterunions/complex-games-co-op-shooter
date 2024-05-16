using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : Resettable
{
    public event Action<ScoreKeeper, int> OnScoreChange;

    private int _score = 0;
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            OnScoreChange?.Invoke(this, Score - value);
        }
    }

    public override void ResetModel()
    {
        _score = 0;
    }
}
