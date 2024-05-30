using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int maxWave;

    private int currentWave = 0;

    [SerializeField] private bool isSpawning = false;

    //[SerializeField] private int enemiesRemaining = 0;

    [SerializeField] private float timer = 0f;
    public float waveSpawnInterval = 100f;

    [Header("Spawner")]
    public Spawner spawner;
    public int _countStart = 19;

    [Header("Show Wave in UI")]
    public Text waveUI;

    [Header("Victory")]
    public GameObject _victory;

    void Start()
    {
        Time.timeScale = 1;
        spawner.maxCount = _countStart;
        StartNextWave();
    }

    void Update()
    {
        waveUI.text = currentWave.ToString() + "/" + maxWave.ToString();

        if (isSpawning)
        {
            timer = 0;
        }
        else
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                if(currentWave > maxWave && spawner.count >= spawner.maxCount)
                {
                    StopSpawning();
                    _victory.SetActive(true);
                }
                else
                {
                    StartNextWave();
                }
            }
        }
    }

    public void EnemyDestroyed()
    {
        if (spawner.count >= spawner.maxCount)
        {
            isSpawning = false;

            timer = waveSpawnInterval;
        }
    }

    void StartNextWave()
    {
        currentWave++;
        spawner.StartNextWave();
        isSpawning = true;

        //----------Spawn Enemy ++
        if(currentWave == 1)
        {
            spawner.maxCount = 20;
        }
        if (currentWave == 2)
        {
            spawner.maxCount = 35;
        }
        if (currentWave == 3)
        {
            spawner.maxCount = 36;
        }
        if (currentWave == 4)
        {
            spawner.maxCount = 71;
        }
        if (currentWave == 5)
        {
            spawner.maxCount = 59;
        }
        if (currentWave == 6)
        {
            spawner.maxCount = 57;
        }
        if (currentWave == 7)
        {
            spawner.maxCount = 75;
        }
        if (currentWave == 8)
        {
            spawner.maxCount = 37;
        }
        if (currentWave == 9)
        {
            spawner.maxCount = 92;
        }
        if (currentWave == 10)
        {
            spawner.maxCount = 90;
        }
        if (currentWave == 11)
        {
            spawner.maxCount = 204;
        }
        if (currentWave == 12)
        {
            spawner.maxCount = 78;
        }
        if (currentWave == 13)
        {
            spawner.maxCount = 80;
        }
        if (currentWave == 14)
        {
            spawner.maxCount = 169;
        }
        if (currentWave == 15)
        {
            spawner.maxCount = 145;
        }
        if (currentWave == 16)
        {
            spawner.maxCount = 151;
        }
        if (currentWave == 17)
        {
            spawner.maxCount = 152;
        }
        if (currentWave == 18)
        {
            spawner.maxCount = 48;
        }
        if (currentWave == 19)
        {
            spawner.maxCount = 240;
        }
        if (currentWave == 20)
        {
            spawner.maxCount = 141;
        }
        if (currentWave == 21)
        {
            spawner.maxCount = 66;
        }
    }

    void StopSpawning()
    {
        spawner.StopSpawning();
        isSpawning = false;
    }
}