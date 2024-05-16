using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOpacity : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField, Range(0, 255)]
    private float _minOpacity = 0;
    [SerializeField, Range(0, 255)] 
    private float _maxOpacity = 255;
    [SerializeField]
    private float _incrementBy = 1f;

    private void Update()
    {
        if(_image.color.a * 255 >= _maxOpacity)
        {
            _incrementBy = -Mathf.Abs(_incrementBy);
        }
        else if(_image.color.a * 255 <= _minOpacity)
        {
            _incrementBy = Mathf.Abs(_incrementBy);
        }
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a + (_incrementBy / 255));
    }
}
