using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed = 1.0f;

    private Vector2 _moveDirection;

    public PlayerGameplayInput playerGameplayInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        _moveDirection = playerGameplayInput.MovementInput;

        _moveDirection = _moveDirection.normalized;

        rb.velocity = _moveDirection * moveSpeed;
    }
}
