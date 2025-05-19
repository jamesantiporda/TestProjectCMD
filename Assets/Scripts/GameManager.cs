using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Camera camera;
    public TMP_Text killCounterText;
    public TMP_Text killCounterText2;
    public GameObject gameOverScreen;
    public TMP_Text clockSeconds;
    public TMP_Text clockMinutes;

    private PlayerGameplayInput _gameplayInput;
    private PlayerUIInput _uiInput;

    public Subject<int> onPlayerDamaged = new Subject<int>();
    public Subject<int> onEnemyDeath = new Subject<int>();
    public Subject<bool> onPlayerDeath = new Subject<bool>();

    private int enemiesKilled;

    private float timeElapsed;
    private float spawnTimer;
    public float spawnTime = 5.0f;

    private float seconds = 0;
    private int minutes = 0;

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
        Time.timeScale = 1.0f;

        gameOverScreen.SetActive(false);

        _gameplayInput = GetComponent<PlayerGameplayInput>();
        _uiInput = GetComponent<PlayerUIInput>();

        _uiInput.enabled = false;

        enemiesKilled = 0;
        timeElapsed = 0.0f;

        onEnemyDeath.Subscribe(death =>
        {
            enemiesKilled += death;
            killCounterText.text = "" + enemiesKilled;
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

        // CLOCK TIMER
        seconds += Time.deltaTime;

        if(seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }

        if(seconds < 10)
        {
            clockSeconds.text = "0" + (int)seconds;
        }
        else
        {
            clockSeconds.text = "" + (int)seconds;
        }

        if (minutes < 10)
        {
            clockMinutes.text = "0" + minutes;
        }
        else
        {
            clockMinutes.text = "" + minutes;
        }

        if (gameOverScreen.activeSelf)
        {
            if(_uiInput.ConfirmInput)
            {
                RestartGame();
            }
        }
    }

    public void DamagePlayer(int damage)
    {
        onPlayerDamaged.OnNext(damage);
    }

    private void GameOver()
    {
        _gameplayInput.enabled = false;

        StartCoroutine(GameOverSequence());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SpawnEnemy(int enemyType)
    {
        Vector3 spawnPos = Vector3.zero;

        int spawnSide = Random.Range(0, 4);

        if(spawnSide == 0)
        {
            spawnPos = camera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1 + 0.3f, camera.nearClipPlane));
        }
        else if(spawnSide == 1)
        {
            spawnPos = camera.ViewportToWorldPoint(new Vector3(1 + 0.3f, Random.Range(0f, 1f), camera.nearClipPlane));
        }
        else if(spawnSide == 2)
        {
            spawnPos = camera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 0 - 0.3f, camera.nearClipPlane));
        }
        else if (spawnSide == 3)
        {
            spawnPos = camera.ViewportToWorldPoint(new Vector3(0 - 0.3f, Random.Range(0f, 1f), camera.nearClipPlane));
        }

        GameObject enemy = ObjectPool.Instance.GetPooledEnemy(enemyType);

        if (enemy != null)
        {
            enemy.transform.position = spawnPos;
            enemy.SetActive(true);
        }
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(2);

        gameOverScreen.SetActive(true);

        Time.timeScale = 0.0f;

        _uiInput.enabled = true;

        killCounterText2.text = "Enemies killed: " + enemiesKilled;
    }
}
