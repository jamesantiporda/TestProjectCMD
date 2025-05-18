using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyHitState : EnemyHitState
{
    private float hitTimer = 0.5f;

    public ShieldEnemyHitState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Hit State");

        enemy.MoveEnemy(Vector2.zero);

        enemy.animator.SetBool("ShieldActive", enemy.ReturnIsShieldActive());
        enemy.animator.SetTrigger("Hit");

        hitTimer = 0.5f;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        hitTimer -= Time.deltaTime;

        if (hitTimer <= 0)
        {
            enemy.StateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
