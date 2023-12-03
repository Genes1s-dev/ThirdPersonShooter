using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;
    
    public override void Enter()
    {

    }

    public override void Perform()
    {
        PatrolCycle();
    }

    public override void Exit()
    {

    }

    public void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3f) {
                if (waypointIndex < enemy.waypontList.Count - 1)
                {
                    waypointIndex++;
                } else {
                    waypointIndex = 0;
                }
                enemy.Agent.SetDestination(enemy.waypontList[waypointIndex].position);  
                waitTimer = 0f;              
            }
        }
    }
}
