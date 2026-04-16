using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPool
    {
        public string enemyName;
        public GameObject prefab;
        public int size;
    }

    public List<EnemyPool> pools;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;

    private Dictionary<string,Queue<GameObject>> poolDictionary;


    void Awake()
    {
        poolDictionary = new Dictionary<string,Queue<GameObject>>();

        foreach(var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.enemyName,objectPool);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int randomPoolIndex = Random.Range(0,pools.Count);
            string targetKey = pools[randomPoolIndex].enemyName;

            SpawnFromPool(targetKey);
        }

        
    }

    void SpawnFromPool(string key)
    {
        if(!poolDictionary.ContainsKey(key)) return;
        
        GameObject objToSpawn = poolDictionary[key].Dequeue();

        if (objToSpawn.activeSelf)
        {
            poolDictionary[key].Enqueue(objToSpawn);
            return;
        }

        int spawnIndex = Random.Range(0,spawnPoints.Length);
        objToSpawn.transform.position = spawnPoints[spawnIndex].position;
        objToSpawn.SetActive(true);

        poolDictionary[key].Enqueue(objToSpawn);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
