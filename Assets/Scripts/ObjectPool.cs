using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private List<GameObject> pooledBasicEnemies = new List<GameObject>();
    private int amountToPool = 1000;

    [SerializeField] private GameObject basicEnemyPrefab;

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
            
        }
        else if(enemyType == 2)
        {

        }

        return null;
    }
}
