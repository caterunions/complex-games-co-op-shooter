using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageOverlay : MonoBehaviour
{
    [SerializeField]
    private DamageReceiver _playerDamageReceiver;
    [SerializeField]
    private Image _damageOverlay;
    [SerializeField]
    private float _fadeTime = 0.2f;

    private Coroutine _overlayRoutine;

    private void OnEnable()
    {
        _damageOverlay.color = new Color(1, 1, 1, 0);
        _playerDamageReceiver.OnDamage += FlashOverlay;
    }

    private void OnDisable()
    {
        _playerDamageReceiver.OnDamage -= FlashOverlay;
    }

    private void FlashOverlay(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        if(_overlayRoutine != null) StopCoroutine(_overlayRoutine);
        _overlayRoutine = StartCoroutine(OverlayRoutine());
    }

    private IEnumerator OverlayRoutine()
    {
        _damageOverlay.color = new Color(1, 1, 1, 1);

        float startTime = Time.time;
        float endTime = startTime + _fadeTime;

        while (Time.time < endTime)
        {
            _damageOverlay.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, Mathf.InverseLerp(startTime, endTime, Time.time)));
            yield return null;
        }
        _damageOverlay.color = new Color(1, 1, 1, 0);

        _overlayRoutine = null;
    }
}
