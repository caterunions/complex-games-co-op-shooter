using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireRoutine : MonoBehaviour
{
    public abstract IEnumerator Sequence(Weapon weapon, Action<Quaternion> onFire);
}

public class SequenceFire : BaseFireControl
{
    private List<FireRoutine> routines;

    public override void Fire(Weapon weapon, Action<Quaternion> onFire, Action onComplete)
    {
        StartCoroutine(RunSequence(weapon, onFire, onComplete));
    }

    private IEnumerator RunSequence(Weapon weapon, Action<Quaternion> onFire, Action onComplete)
    {
        foreach (var r in routines)
        {
            yield return StartCoroutine(r.Sequence(weapon, onFire));
        }
        onComplete();
    }
}