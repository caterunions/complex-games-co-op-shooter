using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOnTriggerEnter : MonoBehaviour
{
    public event Action<EventOnTriggerEnter, Collider> OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        OnTrigger?.Invoke(this, other);
    }
}
