using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner Instance {get;private set;}
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

    protected Dictionary<string, Queue<GameObject>> poolDictionary;

    public ItemTable itemTable;


    protected virtual void Awake()
    {
        Instance = this;
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

        itemTable.Initialize();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
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

    protected GameObject SpawnFromPool(string key)
    {
        if(!poolDictionary.ContainsKey(key)) return null;
        
        GameObject objToSpawn = poolDictionary[key].Dequeue();

        if (objToSpawn.activeSelf)
        {
            poolDictionary[key].Enqueue(objToSpawn);
            return null;
        }

        int spawnIndex = Random.Range(0,spawnPoints.Length);
        objToSpawn.transform.position = spawnPoints[spawnIndex].position;
        objToSpawn.SetActive(true);

        poolDictionary[key].Enqueue(objToSpawn);

        return objToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
