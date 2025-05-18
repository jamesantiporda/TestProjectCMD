using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Camera camera;

    private PlayerGameplayInput _gameplayInput;

    public Subject<int> onPlayerDamaged = new Subject<int>();
    public Subject<int> onEnemyDeath = new Subject<int>();
    public Subject<bool> onPlayerDeath = new Subject<bool>();

    private int enemiesKilled;

    private float timeElapsed;
    private float spawnTimer;
    public float spawnTime = 5.0f;

    public float shieldSpawnStart = 10f;
    private float shieldSpawnTimer;
    public float shieldSpawnTime = 5.0f;

    public float bossSpawnStart = 20f;
    private float bossSpawnTimer;
    public float bossSpawnTime = 10f;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameplayInput = GetComponent<PlayerGameplayInput>();
        enemiesKilled = 0;
        timeElapsed = 0.0f;

        onEnemyDeath.Subscribe(death =>
        {
            enemiesKilled += death;
            Debug.Log("Enemies killed: " + enemiesKilled);
        });

        onPlayerDeath.Subscribe(isDead => GameOver());
    }

    // Update is called once per frame
    void Update()
    {
        // SPAWN TIMERS
        timeElapsed += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        
        if(timeElapsed >= shieldSpawnStart)
        {
            shieldSpawnTimer += Time.deltaTime;
        }

        if(timeElapsed >= bossSpawnStart)
        {
            bossSpawnTimer += Time.deltaTime;
        }

        if(spawnTimer > spawnTime)
        {
            spawnTimer = 0.0f;

            // SPAWN ENEMY
            SpawnEnemy(0);
        }

        if(shieldSpawnTimer > shieldSpawnTime)
        {
            shieldSpawnTimer = 0.0f;

            // SPAWN SHIELD ENEMY
            SpawnEnemy(1);
        }

        if(bossSpawnTimer > bossSpawnTime)
        {
            bossSpawnTimer = 0.0f;

            // SPAWN BOSS ENEMY
            SpawnEnemy(2);
        }

        if(spawnTime > 0.1f)
        {
            spawnTime -= Time.deltaTime / 100;
        }

        if(shieldSpawnTime > 0.1f)
        {
            shieldSpawnTime -= Time.deltaTime / 100;
        }
    }

    public void DamagePlayer(int damage)
    {
        onPlayerDamaged.OnNext(damage);
    }

    public void GameOver()
    {
        _gameplayInput.enabled = false;
    }

    private void SpawnEnemy(int enemyType)
    {
        Vector3 spawnPos = Vector3.zero;

        int spawnSide = Random.Range(0, 4);

        if(spawnSide == 0)
        {
            spawnPos = camera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1 + 0.1f, camera.nearClipPlane));
        }
        else if(spawnSide == 1)
        {
            spawnPos = camera.ViewportToWorldPoint(new Vector3(1 + 0.1f, Random.Range(0f, 1f), camera.nearClipPlane));
        }
        else if(spawnSide == 2)
        {
            spawnPos = camera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 0 - 0.1f, camera.nearClipPlane));
        }
        else if (spawnSide == 3)
        {
            spawnPos = camera.ViewportToWorldPoint(new Vector3(0 - 0.1f, Random.Range(0f, 1f), camera.nearClipPlane));
        }

        GameObject enemy = ObjectPool.Instance.GetPooledEnemy(enemyType);

        if (enemy != null)
        {
            enemy.transform.position = spawnPos;
            enemy.SetActive(true);
        }
    }
}
