using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    private Transform _playerTransform;
    private float _movementSpeed = 1.75f;
    private float attackRange = 1.5f;

    public EnemyMoveState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Move State");

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
        enemy.MoveEnemy(moveDirection * _movementSpeed);

        if(Vector2.Distance(targetPosition, enemyPosition2D) <= attackRange)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
