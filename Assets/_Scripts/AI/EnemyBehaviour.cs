using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;  

public enum EnemyState
{
    Wander,
    FollowTarget,
    Patrol,
    Attack
}
public class EnemyBehaviour : MonoBehaviour
{
    [field:SerializeField] //Only affects the field underneath directly
    public Transform target {get; private set; } // We want to make sure that the target is only set by this class

    [Header("Current State")]
    [SerializeField] private EnemyState state;

    [SerializeField] private float detectionRadius;

    [SerializeField] private AIBase[] states;

    private void Start()
    {
        states = GetComponents<AIBase>(); 
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Wander:
                UpdateWander();
                return;
            case EnemyState.Patrol:
                UpdatePatrol();
                return;
            case EnemyState.FollowTarget:
                UpdateFollowTarget();
                return;
            case EnemyState.Attack:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void UpdatePatrol()
    {
        if (!PlayerisOnRange(detectionRadius)) return;

        ChangeState(EnemyState.FollowTarget);
    }

    private void UpdateWander()
    {
        if (!PlayerisOnRange(detectionRadius)) return;

        ChangeState(EnemyState.FollowTarget);
    }

    private void UpdateFollowTarget()
    {
        if (PlayerisOnRange(detectionRadius)) return;
        
        var dice = Random.Range(0, 100);
        ChangeState(dice >= 50 ? EnemyState.Wander : EnemyState.Patrol); //weird ass if and else
    }

    private void ChangeState(EnemyState newState)
    {
        state = newState;

        for (int i = 0; i < states.Length; i++)
        {
            states[i].enabled = i == (int)state;
        }
    }

    private bool PlayerisOnRange(float detectionRange)
    {
        var sqrDistance = (target.position - transform.position).sqrMagnitude;
        return sqrDistance <= Mathf.Pow(detectionRange, 2);

    }
}
