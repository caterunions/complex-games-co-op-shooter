using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class RetainTrailOnDestroy : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer _trail;

    void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        _trail.transform.parent = null;
        _trail.autodestruct = true;
        Destroy(_trail.gameObject, _trail.time);
    }
}
