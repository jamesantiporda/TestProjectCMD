using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerGameplayInput playerGameplayInput;

    public float shootingCooldown = 0.3f;
    public int playerDamage = 1;
    private float cooldownTimer;

    public LayerMask enemyLayer;

    public GameObject gun;
    public Transform gunTip;
    public Transform laserEnd;

    private Transform[] points;
    [SerializeField] private LineController line;

    // Start is called before the first frame update
    void Start()
    {
        points = new Transform[2];

        points[0] = gunTip;
        points[1] = laserEnd;
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldownTimer <= 0f && playerGameplayInput.AttackInput)
        {
            //Debug.Log("Attack!");
            cooldownTimer = shootingCooldown;

            // Check if Player attack hits an enemy
            RaycastHit2D hit = Physics2D.Raycast(transform.position, gun.transform.right, 100f, enemyLayer);

            if(hit)
            {
                //Debug.Log("hit!");

                // Set Laser end to point of collision
                laserEnd.transform.position = hit.point;

                StartCoroutine(LaserFire());

                Enemy hit_enemy = hit.transform.gameObject.GetComponent<Enemy>();

                hit_enemy.Damage(playerDamage);
            }
            else
            {
                Vector3 dir = new Vector3(playerGameplayInput.LookInput.x, playerGameplayInput.LookInput.y, 0.0f) - Camera.main.WorldToScreenPoint(transform.position);

                // Set Laser end to a far point in the direction aimed at
                laserEnd.transform.position = gunTip.transform.position + dir * 100;

                StartCoroutine(LaserFire());
            }
        }

        if(cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        line.SetUpLine(points);
    }

    private IEnumerator LaserFire()
    {
        // Turn off laser after 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        laserEnd.transform.position = gunTip.transform.position;
    }
}
