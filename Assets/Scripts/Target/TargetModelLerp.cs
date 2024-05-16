using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetModelLerp : MonoBehaviour
{
    [SerializeField]
    private DamageReceiver _targetDR;
    [SerializeField]
    private GameObject _model;
    [SerializeField]
    private LerpBetweenSizes _targetLerp;

    private void OnEnable()
    {
        if (_model == null) _model = gameObject;

        _targetDR.OnDamage += PlayHitFX;
    }

    private void OnDisable()
    {
        _targetDR.OnDamage -= PlayHitFX;
    }

    private void PlayHitFX(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        if(result.Killed)
        {
            _model.transform.parent = null;
            _targetLerp.StartLerp(2.0f, 2.5f, 0.2f);
            Destroy(_model, 0.2f);
        }
        else
        {
            _targetLerp.StartLerp(2.0f, 1.3f, 0.3f);
        }
    }
}