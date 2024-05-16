using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ApplyPixelation : MonoBehaviour
{
    [SerializeField]
    private Shader _pixelateShader;

    [SerializeField]
    private int _screenWidth;
    [SerializeField]
    private int _screenHeight;

    private Material _pixelateMat;

    private void OnEnable()
    {
        if (_pixelateMat == null)
        {
            _pixelateMat = new Material(_pixelateShader);
            _pixelateMat.hideFlags = HideFlags.HideAndDontSave;
        }

        Camera cam = GetComponent<Camera>();
        cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;
    }

    [ImageEffectOpaque]
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _pixelateMat.SetFloat("_PixelNumberX", _screenWidth);
        _pixelateMat.SetFloat("_PixelNumberY", _screenHeight);
        Graphics.Blit(source, destination, _pixelateMat);
    }
}
