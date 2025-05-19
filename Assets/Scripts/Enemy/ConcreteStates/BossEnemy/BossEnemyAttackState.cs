using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyAttackState : EnemyAttackState
{
    public BossEnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        //Debug.Log("Attack State");

        enemy.MoveEnemy(Vector2.zero);

        int random_attack = Random.Range(0, 2);

        enemy.animator.SetInteger("AttackType", random_attack);

        enemy.animator.SetTrigger("Attack");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
