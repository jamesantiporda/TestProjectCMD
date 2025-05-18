using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    private float deathTimer = 0.0f;
    private float deathTime = 0.5f;

    public EnemyDeathState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Death State");

        deathTimer = 0.0f;

        enemy.MoveEnemy(Vector2.zero);

        enemy.animator.SetTrigger("Death");

        GameManager.Instance.onEnemyDeath.OnNext(1);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        deathTimer += Time.deltaTime;

        if(deathTimer >= deathTime)
        {
            enemy.ResetEnemy();
            enemy.gameObject.SetActive(false);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
