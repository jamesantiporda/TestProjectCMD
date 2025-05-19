using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;

    public int maxHealth = 100;
    public int health;
    public float iFrameTime = 2;

    private float iFrameTimer;
    private bool invincible;
    private bool isDead;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        iFrameTimer = 0.0f;
        invincible = false;
        isDead = false;

        anim = GetComponent<Animator>();

        GameManager.Instance.onPlayerDamaged.Subscribe(damage => SubtractHealth(damage)).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(invincible)
        {
            iFrameTimer -= Time.deltaTime;

            if (iFrameTimer <= 0.0f)
            {
                invincible = false;
            }
        }
    }

    public void SubtractHealth(int amount)
    {
        if(!invincible && !isDead)
        {
            health -= amount;

            healthBar.SetHealth(health);

            if(health <= 0)
            {
                anim.SetTrigger("Die");
                isDead = true;
                GameManager.Instance.onPlayerDeath.OnNext(true);
            }
            else
            {
                anim.SetTrigger("Hit");

                invincible = true;
                iFrameTimer = iFrameTime;
            }
        }
    }

    public bool ReturnIsDead()
    {
        return isDead;
    }
}
