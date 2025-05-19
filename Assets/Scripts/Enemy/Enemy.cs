using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb { get; set; }

    public Animator animator { get; set; }

    public GameObject enemySprite;

    protected Transform target;

    #region Enemy State Machine Components
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyMoveState MoveState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyHitState HitState { get; set; }
    public EnemyDeathState DeathState { get; set; }
    #endregion

    // Enemy attributes
    public int maxHealth = 3;
    public int health = 3;
    public int damage = 10;

    public GameObject shield;

    public int maxShieldHealth = 2;
    protected int shieldHealth = 2;
    protected bool shieldActive = true;

    public float movementSpeed = 1.75f;
    public float rotationSpeed = 1f;
    public float attackRange = 1.5f;

    // Attack Cone of Vision values
    public float attackDistance = 2f;
    public float attackAngle = 20f;

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        MoveState = new EnemyMoveState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        HitState = new EnemyHitState(this, StateMachine);
        DeathState = new EnemyDeathState(this, StateMachine);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StateMachine.Initialize(MoveState);

        target = GameObject.FindGameObjectWithTag("Player").transform;

        health = maxHealth;

        shieldHealth = maxShieldHealth;

        shieldActive = true;

        animator.SetBool("ShieldActive", shieldActive);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    public void MoveEnemy(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    public void LookAtPoint(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        enemySprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void EndAttack()
    {
        StateMachine.ChangeState(MoveState);
    }

    public virtual void Damage(int val)
    {
        if(StateMachine.CurrentEnemyState != DeathState)
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
    }

    public void CheckHitSuccess()
    {
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        if(Vector2.Angle(enemySprite.transform.right, directionToTarget) < attackAngle / 2)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if(distanceToTarget <= attackDistance)
            {
                GameManager.Instance.DamagePlayer(damage);
            }
        }
    }

    public void LookAtTarget(Vector3 targetPos)
    {
        Vector3 dir = new Vector3(targetPos.x, targetPos.y, 0.0f) - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

        enemySprite.transform.rotation = Quaternion.Lerp(enemySprite.transform.rotation, rot, Time.deltaTime * rotationSpeed);
    }

    public virtual void ResetEnemy()
    {
        StateMachine.ChangeState(MoveState);

        health = maxHealth;
    }

    public void ActivateShield()
    {
        shieldActive = true;

        animator.SetBool("ShieldActive", shieldActive);
    }

    public void DeactivateShield()
    {
        shieldActive = false;

        animator.SetBool("ShieldActive", shieldActive);
    }

    public bool ReturnIsShieldActive()
    {
        return shieldActive;
    }
}
