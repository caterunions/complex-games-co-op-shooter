using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class ChargeSequence : FireRoutine
{
    [SerializeField]
    private ParticleSystem _chargeFX;
    public ParticleSystem ChargeFX
    {
        private get { return _chargeFX; }
        set { _chargeFX = value; }
    }

    public override IEnumerator Sequence(Weapon weapon, Action<Quaternion> onFire)
    {
        Transform muzzle = weapon.Launcher.Muzzle;
        ParticleSystem chargeFX = Instantiate(ChargeFX, muzzle.position, muzzle.rotation, muzzle);
        Destroy(chargeFX.gameObject, chargeFX.main.duration);
        yield return new WaitForSeconds(chargeFX.main.duration);
    }
}

[Obsolete("Consider building new weapons using SequenceFire controller instead")]
public class ChargeFire : BaseFireControl
{
    private Coroutine _chargeRoutine;

    [SerializeField]
    private ParticleSystem _chargeFX;

    private ChargeSequence _chargeSequence;

    public override void Fire(Weapon weapon, Action<Quaternion> onFire, Action onComplete)
    {
        if(_chargeRoutine == null)
        {
            _chargeRoutine = StartCoroutine(ChargeRoutine(weapon, onFire, onComplete));
        }
    }

    private IEnumerator ChargeRoutine(Weapon weapon, Action<Quaternion> onFire, Action onComplete)
    {
        if(_chargeSequence == null) _chargeSequence = gameObject.AddComponent<ChargeSequence>();
        _chargeSequence.ChargeFX = _chargeFX;

        yield return _chargeSequence.Sequence(weapon, onFire);

        onFire(Quaternion.identity);
        onComplete();

        _chargeRoutine = null;
    }
}