using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public float iFrameTime = 2;

    private float iFrameTimer;
    private bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        iFrameTimer = 0.0f;
        invincible = false;

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
        if(!invincible)
        {
            health -= amount;

            invincible = true;
            iFrameTimer = iFrameTime;
        }
    }
}
