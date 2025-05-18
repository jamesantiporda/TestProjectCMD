using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : Enemy
{
    public override void ResetEnemy()
    {
        StateMachine.ChangeState(MoveState);

        shieldActive = true;

        shield.SetActive(true);

        health = maxHealth;
        shieldHealth = maxShieldHealth;
    }

    public override void Damage(int val)
    {
        if (StateMachine.CurrentEnemyState != DeathState)
        {
            if(!shieldActive)
            {
                health -= val;

                if (health <= 0)
                {
                    StateMachine.ChangeState(DeathState);
                }
                else
                {
                    StateMachine.ChangeState(HitState);
                }
            }
            else
            {
                shieldHealth -= val;

                if(shieldHealth <= 0)
                {
                    shieldActive = false;
                    shield.SetActive(false);
                    animator.SetBool("ShieldActive", shieldActive);
                }

                StateMachine.ChangeState(HitState);
            }
        }
    }
}
