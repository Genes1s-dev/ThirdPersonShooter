using UnityEngine;

public class AttackState : BaseState
{
    public float playerChaseTimer = 0;
    public float reloadingTimer = 0;
    
    public override void Enter()
    {
        enemy.animator.SetTrigger("Hit");
        if (Vector3.Distance(enemy.transform.position, enemy.GetPlayerTransform().position) < enemy.attackRange)
        {
            enemy.GetPlayerTransform().GetComponent<PlayerHealth>().TakeDamage(enemy.damage);
        }
    }

    public override void Perform()
    {
        reloadingTimer += Time.deltaTime;
        if (reloadingTimer > 1f)
        {
            stateMachine.ChangeState(stateMachine.chaseState);
            reloadingTimer = 0f;
        }

    }

    public override void Exit()
    {

    }

 
}
