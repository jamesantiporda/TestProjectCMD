using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerGameplayInput playerGameplayInput;

    public float shootingCooldown = 0.3f;
    private float cooldownTimer;

    public LayerMask enemyLayer;

    public GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldownTimer <= 0f && playerGameplayInput.AttackInput)
        {
            Debug.Log("Attack!");
            cooldownTimer = shootingCooldown;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, gun.transform.right, 100f, enemyLayer);

            if(hit)
            {
                Debug.Log("hit!");

                Enemy hit_enemy = hit.transform.gameObject.GetComponent<Enemy>();

                hit_enemy.Damage(1);
            }
        }

        if(cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
