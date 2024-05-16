using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotNavigation : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private BotStats _stats;

    public bool CanMove { get; set; } = true;
    public float RemainingDistance => _agent.remainingDistance;
    public float OpimalDistance => _stats.OptimalDistance;

    public bool Moving => _agent.velocity.magnitude > 0f;
    public float MoveSpeedAnimationMultiplier
    {
        get
        {
            if (!Moving) return 1f;
            return 0.5f + _agent.velocity.magnitude / 3f;
        }
    }

    private AITargetable _target;

    public void SetTarget(AITargetable target)
    {
        _target = target;
    }

    private void OnEnable()
    {
        _agent.stoppingDistance = _stats.OptimalDistance;
    }

    private void Update()
    {
        if (!CanMove)
        {
            _agent.speed = 0f;
            return;
        }
        _agent.speed = _stats.MoveSpeed;

        if (_target == null) return;
        if (_stats == null) return;

        Vector3 destination = _target.TargetPoint.position;

        _agent.SetDestination(destination);
    }
}
