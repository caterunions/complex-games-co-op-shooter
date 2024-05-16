using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsReveal : MonoBehaviour
{
    [SerializeField]
    private List<RollTextUp> _stats;

    public event Action<StatsReveal> OnStatsFinishedDisplaying;

    private void OnEnable()
    {
        foreach (RollTextUp stat in _stats) stat.gameObject.SetActive(false);
        StartCoroutine(RevealStats());
    }

    private IEnumerator RevealStats()
    {
        foreach (RollTextUp stat in _stats)
        {
            yield return new WaitForSeconds(0.5f);
            stat.gameObject.SetActive(true);
            yield return new WaitUntil(() => stat.CurrentValue == stat.EndValue);
        }
        yield return new WaitForSeconds(0.5f);
        OnStatsFinishedDisplaying?.Invoke(this);
    }
}
