using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageAnimation : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private List<Sprite> _sprites = new List<Sprite>();
    [SerializeField]
    private float _fps = 12;
    [SerializeField]
    private bool _loop = false;

    private int _frame = 0;
    private bool _isPlaying = false;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _isPlaying = true;
        _coroutine = StartCoroutine(Animate());
    }

    private void OnDisable()
    {
        _isPlaying = false;
    }

    private IEnumerator Animate()
    {
        while(_isPlaying)
        {
            _image.sprite = _sprites[_frame];
            _frame++;
            if (_frame >= _sprites.Count) _frame = 0;

            yield return new WaitForSeconds(1f / _fps);

            if(!_loop) _isPlaying = false;
        }
        _coroutine = null;
    }
}
