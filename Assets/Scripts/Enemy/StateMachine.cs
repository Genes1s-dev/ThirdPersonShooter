using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;
    public ChaseState chaseState;
    public AttackState attackState;

    public void Initialize()
    {
        patrolState = new PatrolState();  //создаём экземпляр классов-состояний
        chaseState = new ChaseState();
        attackState = new AttackState();
        ChangeState(patrolState);         //по дефолту, первым состоянием у врага будет патрулирование
    }

    public void PlayerDetection() => ChangeState(chaseState);
  

    private void Update()
    {
        if (activeState != null) {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if (activeState != null) {
            activeState.Exit();
        }

        activeState = newState;
        if (activeState != null) {
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
