using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ApplyFog : MonoBehaviour
{
    [SerializeField]
    private Shader _fogShader;

    [SerializeField]
    private Color _fogColor;
    [SerializeField]
    private float _fogDensity;
    [SerializeField] 
    private float _fogOffset;

    private Material _fogMat;

    private void OnEnable()
    {
        if (_fogMat == null)
        {
            _fogMat = new Material(_fogShader);
            _fogMat.hideFlags = HideFlags.HideAndDontSave;
        }

        Camera cam = GetComponent<Camera>();
        cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;
    }

    [ImageEffectOpaque]
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _fogMat.SetColor("_FogColor", _fogColor);
        _fogMat.SetFloat("_FogDensity", _fogDensity);
        _fogMat.SetFloat("_FogOffset", _fogOffset);
        Graphics.Blit(source, destination, _fogMat);
    }
}
