using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurstSequence : FireRoutine
{
    [SerializeField]
    private int _projectileCount;
    public int ProjectileCount
    {
        get { return _projectileCount; }
        set { _projectileCount = value; }
    }
    [SerializeField]
    private float _delay;
    public float Delay
    {
        get { return _delay; }
        set { _delay = value; }
    }

    public override IEnumerator Sequence(Weapon weapon, Action<Quaternion> onFire)
    {
        for (int i = 0; i < _projectileCount; i++)
        {
            onFire(Quaternion.identity);
            yield return new WaitForSeconds(_delay);
        }
    }
}

[Obsolete("Consider building new weapons using SequenceFire controller instead")]
public class BurstFire : BaseFireControl
{
    [SerializeField]
    private int _projectileCount;
    [SerializeField]
    private float _delay;

    private Coroutine _burstRoutine;

    private BurstSequence _burstSequence;

    public override void Fire(Weapon weapon, Action<Quaternion> onFire, Action onComplete)
    {
        if (_burstRoutine == null)
        {
            _burstRoutine = StartCoroutine(BurstRoutine(weapon, onFire,onComplete));
        }
    }

    private IEnumerator BurstRoutine(Weapon weapon, Action<Quaternion> onFire, Action onComplete)
    {
        if(_burstSequence == null) _burstSequence = gameObject.AddComponent<BurstSequence>();
        _burstSequence.ProjectileCount = _projectileCount;
        _burstSequence.Delay = _delay;

        yield return _burstSequence.Sequence(weapon, onFire);

        onComplete();

        _burstRoutine = null;
    }
}
