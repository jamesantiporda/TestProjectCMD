using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    private Transform _playerTransform;

    public EnemyMoveState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void EnterState()
    {
        base.EnterState();

        //Debug.Log("Move State");

        enemy.animator.SetTrigger("Move");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Vector2 targetPosition = _playerTransform.position;

        Vector2 enemyPosition2D = enemy.transform.position;

        Vector2 moveDirection = (targetPosition - enemyPosition2D).normalized;

        enemy.LookAtPoint(_playerTransform.position);
        enemy.MoveEnemy(moveDirection * enemy.movementSpeed);

        if(Vector2.Distance(targetPosition, enemyPosition2D) <= enemy.attackRange)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
