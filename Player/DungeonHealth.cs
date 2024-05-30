using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonHealth : MonoBehaviour
{
    public int _hp = 5;
    public GameObject gameOver;

    [Header("Show in Health UI")]
    public Text health;

    void Update()
    {
        health.text = _hp.ToString();

        if (_hp == 0)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _hp -= 1;
        }
    }
}
