using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabBurst : MonoBehaviour
{
    [SerializeField]
    private List<PrefabBurstObject> _objects;

    [SerializeField]
    private float _lifetime = 1;
    [SerializeField]
    private float _fadeTime = 1f;

    [SerializeField]
    private float _minVelocity = 1f;
    [SerializeField]
    private float _maxVelocity = 1f;

    [SerializeField]
    private Transform _spawnPoint;

    private void OnEnable()
    {
        if(_spawnPoint == null) _spawnPoint = transform;
    }

    public void CreateBurst(int count, float radius)
    {
        for(int i = 0; i < count; i++)
        {
            PrefabBurstObject chosenObject = _objects[Random.Range(0, _objects.Count)];

            PrefabBurstObject spawnedObj = Instantiate(
                chosenObject,
                _spawnPoint.position,
                Quaternion.Euler(_spawnPoint.rotation.x + Random.Range(-radius, radius), _spawnPoint.rotation.y + Random.Range(-radius, radius), 0f));

            spawnedObj.Spawner = this;
            spawnedObj.Initialize(Random.Range(_minVelocity, _maxVelocity), _lifetime, _fadeTime);
        }
    }
}
