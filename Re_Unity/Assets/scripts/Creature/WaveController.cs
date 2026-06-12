using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WaveController : EnemySpawner
{
    public int enemiesPerWave = 60;
    public int currentWave = 1;
    private int activeEnemyCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
    }


    protected override void Start()
    {
        StartCoroutine(WaveRoutine());
    }

    // Update is called once per frame
    IEnumerator WaveRoutine()
    {
        while (true)
        {
            Debug.Log("웨이브 시작");
            activeEnemyCount = 0;

            for(int i = 0; i < enemiesPerWave; i++)
            {
                int randomPoolIndex = Random.Range(0,pools.Count);
                GameObject enemy = SpawnFromPool(pools[randomPoolIndex].enemyName);
                if(enemy != null)
                {
                    activeEnemyCount++;
                }
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitUntil(() => activeEnemyCount ==0);
            Debug.Log("클리어");
            yield return new WaitForSeconds(5f);
            currentWave++;
        }
    }

    public void EnemyDied()
    {
        activeEnemyCount--;
    }
}
