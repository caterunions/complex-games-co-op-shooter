using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//If Health and Shield pool can be split up, both pools can be viewed by separate instances of the same pool displayer configured with different art.
public class PlayerHealthDisplay : MonoBehaviour
{
    [SerializeField]
    private HealthPool _playerHealthPool;
    [SerializeField]
    private Image _hpMask;
    [SerializeField]
    private Image _topImg;
    [SerializeField]
    private float _topImgMaxY;
    [SerializeField]
    private float _topImgMinY;

    private void OnEnable()
    {
        _playerHealthPool.OnHPChange += UpdateHealthDisplay;
    }

    private void OnDisable()
    {
        _playerHealthPool.OnHPChange -= UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay(HealthPool healthPool, float prevHP)
    {
        float healthPercentage = healthPool.HP / healthPool.MaxHP;

        _hpMask.fillAmount = healthPercentage;

        if (_topImg == null) return;
        if (healthPool.HP <= 0f)
        {
            _topImg.enabled = false;
        }
        else
        {
            _topImg.enabled = true;
            _topImg.rectTransform.anchoredPosition = new Vector2(_topImg.rectTransform.anchoredPosition.x, Mathf.Lerp(_topImgMinY, _topImgMaxY, healthPercentage));
        }
    }
}
