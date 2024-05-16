using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BotBrain))]
public class BotStats : MonoBehaviour
{
    [SerializeField]
    private float _fireDistance = 5f;
    public float FireDistance => _fireDistance;

    [SerializeField]
    private float _maxFireAngle = 20f;
    public float MaxFireAngle => _maxFireAngle;

    [SerializeField]
    private float _rotationSpeed = 180f;
    public float RotationSpeed => _rotationSpeed;

    [SerializeField]
    private LayerMask _sightObstructionLayers;
    public LayerMask SightObstructionLayers => _sightObstructionLayers;

    [SerializeField]
    private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;

    [SerializeField]
    private float _optimalDistance;
    public float OptimalDistance => _optimalDistance;
}