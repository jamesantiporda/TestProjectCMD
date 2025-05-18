using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public float alternateAttackDistance = 5f;
    public float alternateAttackAngle = 30f;

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        MoveState = new EnemyMoveState(this, StateMachine);
        AttackState = new BossEnemyAttackState(this, StateMachine);
        HitState = new EnemyHitState(this, StateMachine);
        DeathState = new EnemyDeathState(this, StateMachine);
    }

    public void CheckHitSuccessAlternate()
    {
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        if (Vector2.Angle(enemySprite.transform.right, directionToTarget) < alternateAttackAngle / 2)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget <= alternateAttackDistance)
            {
                //Debug.Log("PLAYER HIT!");

                GameManager.Instance.DamagePlayer(10);
            }
        }
    }
}
