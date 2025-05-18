using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private List<GameObject> pooledBasicEnemies = new List<GameObject>();
    private List<GameObject> pooledShieldEnemies = new List<GameObject>();
    private List<GameObject> pooledBossEnemies = new List<GameObject>();
    private int amountToPool = 1000;
    private int amountOfShieldToPool = 500;
    private int amountOfBossToPool = 10;

    [SerializeField] private GameObject basicEnemyPrefab;
    [SerializeField] private GameObject shieldEnemyPrefab;
    [SerializeField] private GameObject bossEnemyPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject enemy = Instantiate(basicEnemyPrefab);
            enemy.SetActive(false);
            pooledBasicEnemies.Add(enemy);
        }

        for (int i = 0; i < amountOfShieldToPool; i++)
        {
            GameObject enemy = Instantiate(shieldEnemyPrefab);
            enemy.SetActive(false);
            pooledShieldEnemies.Add(enemy);
        }

        for (int i = 0; i < amountOfBossToPool; i++)
        {
            GameObject enemy = Instantiate(bossEnemyPrefab);
            enemy.SetActive(false);
            pooledBossEnemies.Add(enemy);
        }
    }

    public GameObject GetPooledEnemy(int enemyType)
    {
        if(enemyType == 0)
        {
            for (int i = 0; i < pooledBasicEnemies.Count; i++)
            {
                if (!pooledBasicEnemies[i].gameObject.activeInHierarchy)
                {
                    return pooledBasicEnemies[i];
                }
            }
        }
        else if(enemyType == 1)
        {
            for (int i = 0; i < pooledShieldEnemies.Count; i++)
            {
                if (!pooledShieldEnemies[i].gameObject.activeInHierarchy)
                {
                    return pooledShieldEnemies[i];
                }
            }
        }
        else if(enemyType == 2)
        {
            for (int i = 0; i < pooledBossEnemies.Count; i++)
            {
                if (!pooledBossEnemies[i].gameObject.activeInHierarchy)
                {
                    return pooledBossEnemies[i];
                }
            }
        }

        return null;
    }
}
