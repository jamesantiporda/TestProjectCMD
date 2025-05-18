using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb { get; set; }

    public Animator animator { get; set; }

    public GameObject enemySprite;

    private Transform target;

    #region Enemy State Machine Components
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyMoveState MoveState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyHitState HitState { get; set; }
    public EnemyDeathState DeathState { get; set; }
    #endregion

    public int health = 3;

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
        StateMachine.Initialize(MoveState);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rb.velocity);

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

    public void Damage(int val)
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
                Debug.Log("PLAYER HIT!");

                GameManager.Instance.DamagePlayer(10);
            }
        }
    }
}
