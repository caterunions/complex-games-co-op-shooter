using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ApplyPosterization : MonoBehaviour
{
    [SerializeField]
    private Shader _posterizeShader;

    [SerializeField, Range(1, 10)]
    private int _posterization = 6;

    private Material _posterizeMat;

    private void OnEnable()
    {
        if (_posterizeMat == null)
        {
            _posterizeMat = new Material(_posterizeShader);
            _posterizeMat.hideFlags = HideFlags.HideAndDontSave;
        }

        Camera cam = GetComponent<Camera>();
        cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;
    }

    [ImageEffectOpaque]
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _posterizeMat.SetFloat("_steps", _posterization);
        Graphics.Blit(source, destination, _posterizeMat);
    }
}
