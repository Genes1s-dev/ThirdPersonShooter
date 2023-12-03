using UnityEngine;

public class ChaseState : BaseState
{
    public float playerChaseTimer = 0;
    
    public override void Enter()
    {

    }

    public override void Perform()
    {
        ChasePlayer();
    }

    public override void Exit()
    {

    }

    public void ChasePlayer()
    {
        float distance = Vector3.Distance(enemy.gameObject.transform.position, enemy.GetPlayerTransform().position);
        playerChaseTimer += Time.deltaTime;

        if (playerChaseTimer <= 5f)
        {
            if (distance < 2.5f)
            {
                stateMachine.ChangeState(stateMachine.attackState);
            }
            Vector3 directionToPlayer = (enemy.GetPlayerTransform().position - enemy.gameObject.transform.position).normalized;
            Vector3 destination = enemy.GetPlayerTransform().position - directionToPlayer * enemy.attackRange; //здесь получаем точку между игроком и енеми, к которой будет двигаться последний. Точка находится на достаточном для атаки расстоянии, чтобы враг не сталкивался с игроком 
            enemy.Agent.SetDestination(destination);  
        } else {
            playerChaseTimer = 0f;
            stateMachine.ChangeState(stateMachine.patrolState);
        }
    }
}
