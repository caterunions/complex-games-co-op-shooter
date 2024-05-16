using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DisplayShieldOutline : MonoBehaviour
{
    [SerializeField]
    private Material _shieldMat;
    [SerializeField]
    private List<SkinnedMeshRenderer> _skinnedMeshRenderers;
    [SerializeField]
    private HealthPool _shieldPool;

    private void OnEnable()
    {
        _shieldPool.OnHPChange += UpdateShieldMats;
    }

    private void OnDisable()
    {
        _shieldPool.OnHPChange -= UpdateShieldMats;
    }

    private void UpdateShieldMats(HealthPool shieldPool, float prevShield)
    {
        if (shieldPool.HP > 0f)
        {
            ToggleShieldMats(true);
        }
        else
        {
            ToggleShieldMats(false);
        }
    }

    private void ToggleShieldMats(bool visible)
    {
        foreach(SkinnedMeshRenderer renderer in _skinnedMeshRenderers)
        {
            Material[] mats = renderer.materials;
            mats[mats.Length - 1] = visible ? _shieldMat : mats[mats.Length - 2];
            renderer.materials = mats;
        }
    }
}
