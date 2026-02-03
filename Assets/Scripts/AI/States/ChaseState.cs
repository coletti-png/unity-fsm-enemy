using UnityEngine;

public class ChaseState : IState
{
    EnemyController enemy;
    float lostTimer;
    const float LOST_TIME = 2f;

    public ChaseState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        lostTimer = 0f;
        enemy.UpdateStateText("Chase");
        Debug.Log("State: Chase");
    }

    public void Tick()
    {
        if (enemy.IsPlayerInRange() && enemy.HasLineOfSight())
        {
            enemy.MoveTowards(enemy.player.position, enemy.chaseSpeed);
            lostTimer = 0f;
        }
        else
        {
            lostTimer += Time.deltaTime;

            if (lostTimer >= LOST_TIME)
            {
                enemy.stateMachine.ChangeState(enemy.idleState);
            }
        }
    }

    public void Exit() { }
}
