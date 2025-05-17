using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb { get; set; }

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyMoveState MoveState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyHitState HitState { get; set; }
    public EnemyDeathState DeathState { get; set; }

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
}
