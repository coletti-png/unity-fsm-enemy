using UnityEngine;

public class IdleState : IState
{
    EnemyController enemy;
    float idleTimer;
    const float IDLE_DURATION = 2f;

    public IdleState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        idleTimer = 0f;
        enemy.UpdateStateText("Idle");
        Debug.Log("State: Idle");
    }
    public void Tick()
    {
        idleTimer += Time.deltaTime;

        if (enemy.IsPlayerInRange() && enemy.HasLineOfSight())
        {
            enemy.stateMachine.ChangeState(enemy.chaseState);
            return;
        }

        if (idleTimer >= IDLE_DURATION)
        {
            enemy.stateMachine.ChangeState(enemy.patrolState);
        }
    }

    public void Exit() { }
}