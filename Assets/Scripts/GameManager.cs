using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerGameplayInput _gameplayInput;

    public Subject<int> onPlayerDamaged = new Subject<int>();
    public Subject<int> onEnemyDeath = new Subject<int>();

    private int enemiesKilled;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameplayInput = GetComponent<PlayerGameplayInput>();
        enemiesKilled = 0;

        onEnemyDeath.Subscribe(death =>
        {
            enemiesKilled += death;
            Debug.Log("Enemies killed: " + enemiesKilled);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        onPlayerDamaged.OnNext(damage);
    }

    public void GameOver()
    {
        _gameplayInput.enabled = false;
    }
}
