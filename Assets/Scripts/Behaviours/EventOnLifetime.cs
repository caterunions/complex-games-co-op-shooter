using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOnLifetime : MonoBehaviour
{
    public event Action<EventOnLifetime> OnLifetime;

    [SerializeField]
    private float _lifetime;
    public float Lifetime => _lifetime;

    private Coroutine coroutine;
    private void OnEnable()
    {
        coroutine = StartCoroutine(RunLifetime());
    }

    private void OnDisable()
    {
        if( coroutine != null )
        {
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator RunLifetime()
    {
        yield return new WaitForSeconds(_lifetime);
        OnLifetime?.Invoke(this);
        coroutine = null;
    }
}
