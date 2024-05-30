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
    //public GameObject M_enemyLV3;

    [Header("Spawn limit")]
    public int P1_spawn;
    public int P2_spawn;
    public int P3_spawn;
    public int P4_spawn;
    //public int P5_spawn;
    public int M1_spawn;
    public int M2_spawn;
    //public int M3spawn;

    private int P1_limit;
    private int P2_limit;
    private int P3_limit;
    private int P4_limit;

    private int M1_limit;
    private int M2_limit;

    [Header("wave")]
    public bool nextWave;
    public List<Transform> wayPoints;

    public int maxCount;
    public int count = 0;

    void Lv1Enemy()
    {
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
        GameObject enemy = Instantiate(enemyLV1, transform.position, rotation);//Quaternion.identity);
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
            if (maxCount == 20) //wave 1
            {
                InvokeRepeating("Lv1Enemy", 1, 1f);
                P1_limit = 20;
            }

            if (maxCount == 35) //wave 2
            {
                InvokeRepeating("Lv1Enemy", 1, 1f);
                P1_limit = 35;
            }

            if (maxCount == 36) //wave 3
            {
                InvokeRepeating("Lv1Enemy", 1, 1f);
                InvokeRepeating("Lv2Enemy", 15, 0.5f);
                P1_limit = 26;
                P2_limit = 5;
            }

            if (maxCount == 71) //wave 4
            {
                InvokeRepeating("Lv1Enemy", 1, 0.5f);
                InvokeRepeating("Lv2Enemy", 6, 1f);
                P1_limit = 35;
                P2_limit = 18;
            }

            if (maxCount == 59) //wave 5
            {
                InvokeRepeating("Lv1Enemy", 10, 0.4f);
                InvokeRepeating("Lv2Enemy", 1, 1f);
                P1_limit = 5;
                P2_limit = 27;
            }

            if (maxCount == 57) //wave 6
            {
                InvokeRepeating("Lv1Enemy", 1, 2f);
                InvokeRepeating("Lv2Enemy", 5, 0.6f);
                InvokeRepeating("Lv3Enemy", 10, 1f);
                P1_limit = 15;
                P2_limit = 15;
                P3_limit = 4;
            }

            if (maxCount == 75) //wave 7
            {
                InvokeRepeating("Lv1Enemy", 1, 1f);
                InvokeRepeating("Lv2Enemy", 5, 1f);
                InvokeRepeating("Lv3Enemy", 10, 0.8f);
                P1_limit = 20;
                P2_limit = 20;
                P3_limit = 5;
            }

            if (maxCount == 37) //wave 8 ----------Magic Enemy
            {
                InvokeRepeating("Lv1EnemyM", 1, 1f);
                M1_limit = 37;
            }

            if (maxCount == 92) //wave 9
            {
                InvokeRepeating("Lv1Enemy", 10, 0.5f);
                InvokeRepeating("Lv2Enemy", 1, 1f);
                InvokeRepeating("Lv3Enemy", 18, 0.8f);
                P1_limit = 10;
                P2_limit = 20;
                P3_limit = 14;
            }

            if (maxCount == 90) //wave 10
            {
                InvokeRepeating("Lv3Enemy", 1, 1f);
                P3_limit = 30;
            }

            if (maxCount == 204) //wave 11
            {
                InvokeRepeating("Lv2Enemy", 1, 0.5f);
                P2_limit = 102;
            }

            if (maxCount == 78) //wave 12
            {
                InvokeRepeating("Lv1Enemy", 15, 1f);
                InvokeRepeating("Lv2Enemy", 1, 0.6f);
                InvokeRepeating("Lv3Enemy", 10, 0.8f);
                InvokeRepeating("Lv1EnemyM", 13, 1f);
                P1_limit = 6;
                P2_limit = 12;
                P3_limit = 12;
                M1_limit = 3;
            }

            if (maxCount == 80) //wave 13
            {
                InvokeRepeating("Lv2Enemy", 1, 0.6f);
                InvokeRepeating("Lv3Enemy", 10, 0.8f);
                InvokeRepeating("Lv1EnemyM", 13, 1f);
                P2_limit = 15;
                P3_limit = 10;
                M1_limit = 5;
            }

            if (maxCount == 169) //wave 14
            {
                InvokeRepeating("Lv2Enemy", 1, 0.8f);
                InvokeRepeating("Lv3Enemy", 20, 0.6f);
                P2_limit = 50;
                P3_limit = 23;
            }

            if (maxCount == 145) //wave 15
            {
                InvokeRepeating("Lv1Enemy", 1, 0.8f);
                InvokeRepeating("Lv2Enemy", 1, 0.8f);
                InvokeRepeating("Lv3Enemy", 20, 0.6f);
                InvokeRepeating("Lv1EnemyM", 1, 0.8f);
                P1_limit = 49;
                P2_limit = 15;
                P3_limit = 10;
                M1_limit = 9;
            }

            if (maxCount == 151) //wave 16
            {
                InvokeRepeating("Lv1Enemy", 1, 0.8f);
                InvokeRepeating("Lv2Enemy", 1, 0.8f);
                InvokeRepeating("Lv3Enemy", 20, 0.6f);
                InvokeRepeating("Lv1EnemyM", 1, 0.8f);
                InvokeRepeating("Lv2EnemyM", 10, 0.8f);
                P1_limit = 20;
                P2_limit = 15;
                P3_limit = 12;
                M1_limit = 10;
                M2_limit = 13;
            }

            if (maxCount == 152) //wave 17
            {
                InvokeRepeating("Lv3Enemy", 1, 0.6f);
                InvokeRepeating("Lv1EnemyM", 8, 0.8f);
                P3_limit = 40;
                M1_limit = 8;
            }

            if (maxCount == 48) //wave 18
            {
                InvokeRepeating("Lv2EnemyM", 1, 0.8f);
                M2_limit = 24;
            }

            if (maxCount == 240) //wave 19
            {
                InvokeRepeating("Lv3Enemy", 1, 0.6f);
                P3_limit = 80;
            }

            if (maxCount == 141) //wave 20
            {
                InvokeRepeating("Lv3Enemy", 1, 0.6f);
                InvokeRepeating("Lv1EnemyM", 8, 0.8f);
                InvokeRepeating("Lv2EnemyM", 13, 0.8f);
                P3_limit = 33;
                M1_limit = 4;
                M2_limit = 13;
            }
        }
        nextWave = false;

        if (count >= maxCount)
        {
            CancelInvoke();

            //----------reset limit
            P1_limit = 0;
            P2_limit = 0;
            P3_limit = 0;
            P4_limit = 0;
            //P5_limit = 0;
            M1_limit = 0;
            M2_limit = 0;
            //----------reset spawn
            P1_spawn = 0;
            P2_spawn = 0;
            P3_spawn = 0;
            P4_spawn = 0;
            //P5_spawn = 0;
            M1_spawn = 0;
            M2_spawn = 0;
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