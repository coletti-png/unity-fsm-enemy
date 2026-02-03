using UnityEngine;

public class PatrolState : IState
{
    EnemyController enemy;
    int currentPointIndex;

    public PatrolState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        currentPointIndex = 0;
        enemy.UpdateStateText("Patrol");
        Debug.Log("State: Patrol");
    }

    public void Tick()
    {
        if (enemy.IsPlayerInRange() && enemy.HasLineOfSight())
        {
            enemy.stateMachine.ChangeState(enemy.chaseState);
            return;
        }

        if (enemy.patrolPoints.Length == 0) return;

        Transform target = enemy.patrolPoints[currentPointIndex];
        enemy.MoveTowards(target.position, enemy.patrolSpeed);

        if (Vector3.Distance(enemy.transform.position, target.position) < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % enemy.patrolPoints.Length;
        }
    }

    public void Exit() { }
}
