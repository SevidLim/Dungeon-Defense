using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Phycis Enemy Prefab")]
    public GameObject enemyLV1;
    public GameObject enemyLV2;
    public GameObject enemyLV3;
    public GameObject enemyLV4;

    [Header("Magic Enemy Prefab")]
    public GameObject M_enemyLV1;
    public GameObject M_enemyLV2;

    [Header("Spawn limit")]
    public int P1_spawn;
    public int P2_spawn;
    public int P3_spawn;
    public int P4_spawn;

    public int M1_spawn;
    public int M2_spawn;

    private int P1_limit;
    private int P2_limit;
    private int P3_limit;
    private int P4_limit;

    private int M1_limit;
    private int M2_limit;

    [Header("wave")]
    public int currentWave;
    private int newWave;
    public bool nextWave;
    public List<Transform> wayPoints;

    public int maxCount;
    public int count = 0;

    [Header("Spawn Data")]
    public List<WaveConfig> waveConfigs;

    void Lv1Enemy()
    {
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
        GameObject enemy = Instantiate(enemyLV1, transform.position, rotation);
        enemy.GetComponent<EnemyController>().SetDestination(wayPoints);

        count++;
        P1_spawn++;
        if(P1_spawn >= P1_limit)
        {
            CancelInvoke("Lv1Enemy");
        }
    }

    void Lv2Enemy()
    {
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
        GameObject enemy = Instantiate(enemyLV2, transform.position, rotation);
        enemy.GetComponent<EnemyController>().SetDestination(wayPoints);

        count+= 2;
        P2_spawn++;
        if (P2_spawn >= P2_limit)
        {
            CancelInvoke("Lv2Enemy");
        }
    }

    void Lv3Enemy()
    {
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
        GameObject enemy = Instantiate(enemyLV3, transform.position, rotation);
        enemy.GetComponent<EnemyController>().SetDestination(wayPoints);

        count+= 3;
        P3_spawn++;
        if (P3_spawn >= P3_limit)
        {
            CancelInvoke("Lv3Enemy");
        }
    }

    void Lv4Enemy()
    {
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
        GameObject enemy = Instantiate(enemyLV4, transform.position, rotation);
        enemy.GetComponent<EnemyController>().SetDestination(wayPoints);

        count += 5;
        P4_spawn++;
        if (P4_spawn >= P4_limit)
        {
            CancelInvoke("Lv4Enemy");
        }
    }

    //-------------------------Magic Enemy
    void Lv1EnemyM()
    {
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
        GameObject enemy = Instantiate(M_enemyLV1, transform.position, rotation);
        enemy.GetComponent<EnemyController>().SetDestination(wayPoints);

        count += 4;
        M1_spawn++;
        if (M1_spawn >= M1_limit)
        {
            CancelInvoke("Lv1EnemyM");
        }
    }

    void Lv2EnemyM()
    {
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
        GameObject enemy = Instantiate(M_enemyLV1, transform.position, rotation);
        enemy.GetComponent<EnemyController>().SetDestination(wayPoints);

        count+= 2;
        M2_spawn++;
        if (M2_spawn >= M2_limit)
        {
            CancelInvoke("Lv2EnemyM");
        }
    }

    void Update()
    {
        if (nextWave)
        {
            maxCount = waveConfigs[currentWave].maxCount;

            if (waveConfigs[currentWave].maxCount == maxCount)
            {
                foreach (var spawn in waveConfigs[currentWave].enemySpawn)
                {
                    InvokeRepeating(spawn.methodName, spawn.startSpawnTime, spawn.interval);
                    SetLimit(spawn.methodName, spawn.limit);
                }
            }
        }

        nextWave = false;

        if (count >= maxCount)
        {
            //----------reset limit
            P1_limit = 0;
            P2_limit = 0;
            P3_limit = 0;
            P4_limit = 0;

            M1_limit = 0;
            M2_limit = 0;
            
            //----------reset spawn
            P1_spawn = 0;
            P2_spawn = 0;
            P3_spawn = 0;
            P4_spawn = 0;

            M1_spawn = 0;
            M2_spawn = 0;
        }
    }

    private void SetLimit(string methodName, int limit)
    {
        switch (methodName)
        {
            case "Lv1Enemy":
                P1_limit = limit;
                break;
            case "Lv2Enemy":
                P2_limit = limit;
                break;
            case "Lv3Enemy":
                P3_limit = limit;
                break;
            case "Lv4Enemy":
                P4_limit = limit;
                break;
            case "Lv1EnemyM":
                M1_limit = limit;
                break;
            case "Lv2EnemyM":
                M2_limit = limit;
                break;
        }
    }

    public void StartNextWave()
    {
        count = 0;
        nextWave = true;
    }

    public void StopSpawning()
    {
        CancelInvoke();
    }
}

[System.Serializable]
public class WaveConfig
{
    public string wave;
    public int maxCount;
    public List<EnemySpawn> enemySpawn;
}

[System.Serializable]
public class EnemySpawn
{
    public string methodName;
    public int startSpawnTime;
    public float interval;
    public int limit;
}