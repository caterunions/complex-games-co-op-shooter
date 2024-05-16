using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ApplyInitialForwardMomentum)), RequireComponent(typeof(LerpBetweenSizes))]
public class PrefabBurstObject : MonoBehaviour
{
    public PrefabBurst Spawner { get; set; }

    private ApplyInitialForwardMomentum _applyMomentum;
    public ApplyInitialForwardMomentum ApplyMomentum
    {
        get
        {
            if (_applyMomentum == null) _applyMomentum = GetComponent<ApplyInitialForwardMomentum>();
            return _applyMomentum;
        }
    }

    private LerpBetweenSizes _lerp;
    public LerpBetweenSizes Lerp
    {
        get
        {
            if (_lerp == null) _lerp = GetComponent<LerpBetweenSizes>();
            return _lerp;
        }
    }

    [SerializeField]
    private DamageReceiver _damageReceiver;

    private void OnEnable()
    {
        if (_damageReceiver != null) _damageReceiver.OnDamage += TryDestroy;
    }

    private void OnDisable()
    {
        if (_damageReceiver != null) _damageReceiver.OnDamage -= TryDestroy;
    }

    public void Initialize(float speed, float lifetime, float fadeTime)
    {
        Lerp.OnComplete += DestroyAfterLerp;
        StartCoroutine(StartLerpDelayed(lifetime, fadeTime));

        ApplyMomentum.Speed = speed;
        ApplyMomentum.enabled = true;
    }

    private IEnumerator StartLerpDelayed(float lifetime, float fadeTime)
    {
        yield return new WaitForSeconds(lifetime);
        Lerp.StartLerp(Lerp.transform.localScale.x, 0f, fadeTime);
    }

    private void TryDestroy(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        if(result.Killed)
        {
            Destroy(gameObject);
        }
    }

    private void DestroyAfterLerp(LerpBetweenSizes lerp)
    {
        Lerp.OnComplete -= DestroyAfterLerp;
        Destroy(gameObject);
    }
}
