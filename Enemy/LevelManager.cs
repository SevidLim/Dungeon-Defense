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

    [SerializeField] private float timer = 0f;
    public float waveSpawnInterval;

    [Header("Spawner")]
    public Spawner spawner;

    [Header("Show Wave in UI")]
    public Text waveUI;

    [Header("Victory")]
    public GameObject _victory;

    void Start()
    {
        Time.timeScale = 1;
        StartNextWave();
    }

    void Update()
    {
        waveUI.text = currentWave.ToString() + "/" + maxWave.ToString();

        spawner.currentWave = currentWave;

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
    }

    void StopSpawning()
    {
        spawner.StopSpawning();
        isSpawning = false;
    }
}