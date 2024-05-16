using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHealthDisplay : MonoBehaviour
{
    [SerializeField]
    private HealthPool _healthPool;
    [SerializeField]
    private SpriteRenderer _healthBar;

    private void OnEnable()
    {
        _healthPool.OnHPChange += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _healthPool.OnHPChange -= UpdateHealthBar;
    }

    private void UpdateHealthBar(HealthPool healthPool, float prevHP)
    {
        float percentage = healthPool.HP / healthPool.MaxHP;
        float invertedPercentage = 1 - percentage;

        _healthBar.transform.localPosition = new Vector3(-invertedPercentage / 2, _healthBar.transform.localPosition.y, _healthBar.transform.localPosition.z);
        _healthBar.transform.localScale = new Vector3(percentage, _healthBar.transform.localScale.y, _healthBar.transform.localScale.z);
    }
}
