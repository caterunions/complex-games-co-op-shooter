using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetainParticlesOnDestroy : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particles;

    void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        _particles.transform.parent = null;

        ParticleSystem.MainModule main = _particles.main;
        main.loop = false;
        main.stopAction = ParticleSystemStopAction.Destroy;
        Destroy(_particles.gameObject, _particles.main.duration);
    }
}
