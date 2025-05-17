using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed = 1.0f;

    private Vector2 _moveDirection;

    public PlayerGameplayInput playerGameplayInput;

    public GameObject playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Mouse x: " + playerGameplayInput.LookInput.x + " | Mouse y: " + playerGameplayInput.LookInput.y);

        LookAtMouse();
    }

    private void FixedUpdate()
    {
        _moveDirection = playerGameplayInput.MovementInput;

        _moveDirection = _moveDirection.normalized;

        rb.velocity = _moveDirection * moveSpeed;
    }

    private void LookAtMouse()
    {
        Vector3 dir = new Vector3(playerGameplayInput.LookInput.x, playerGameplayInput.LookInput.y, 0.0f) - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        playerSprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
