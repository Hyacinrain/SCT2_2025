using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollowTarget : AIBase
{
    private void Update()
    {
        agent.SetDestination(enemyBehaviour.target.position);
    }
}
