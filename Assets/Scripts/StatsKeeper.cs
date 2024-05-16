using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsKeeper : Resettable
{
    private int _targetsHit;
    public int TargetsHit
    {
        get { return _targetsHit; }
        set { _targetsHit = value; }
    }
    private int _shotsFired;
    public int ShotsFired
    {
        get { return _shotsFired; }
        set { _shotsFired = value; }
    }
    public int Accuracy
    {
        get
        {
            if (ShotsFired == 0) return 0;
            return Mathf.RoundToInt(((float)TargetsHit / (float)ShotsFired) * 100f);
        }
    }

    public override void ResetModel()
    {
        TargetsHit = 0;
        ShotsFired = 0;
    }
}
