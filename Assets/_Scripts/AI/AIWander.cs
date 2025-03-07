using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIWander : AIBase
{
    [SerializeField] private float wanderRadius;
    private Vector3 _initialposition;
    private Vector3 _randomEndPoint;

    protected override void Start()
    {
        base.Start();

        _initialposition = transform.position;
        SetNewRandomPoint();
    }

    private void Update()
    {
        if (!agent.enabled) return;

        if (agent.remainingDistance <= breakingDistance && !agent.pathPending)
        {
            SetNewRandomPoint();
        }
    }
    private void SetNewRandomPoint()
    {
        _randomEndPoint = _initialposition + Random.insideUnitSphere * wanderRadius;
        _randomEndPoint.y = 0; // We don't want the enemy to wander up or down
        
        agent.SetDestination(_randomEndPoint);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _initialposition = transform.position;
    }

    private void OnDrawGizmos()
    {
        if (agent != null)
        {
            Gizmos.DrawWireSphere(_randomEndPoint, 0.5f);
            Gizmos.DrawWireSphere(agent.destination, 0.5f);
        }
    }
}
