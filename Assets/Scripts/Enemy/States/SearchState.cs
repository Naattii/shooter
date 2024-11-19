using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    public override void Enter()
    {
        enemy.NavMeshAgent.SetDestination(enemy.LastKnownPlayerPosition);
    }

    public override void Perform()
    {
        if(enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
        if (enemy.NavMeshAgent.remainingDistance < enemy.NavMeshAgent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            if (moveTimer > Random.Range(2, 5))
                {
                enemy.NavMeshAgent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));
                moveTimer = 0;
                }
            if (searchTimer > 8)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public override void Exit()
    {

    }
}
