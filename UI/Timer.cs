using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject _object;
    public float timer;
    [SerializeField] private float _timer;

    void Start()
    {
        _timer = timer;
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = _timer;
            _object.SetActive(false);
        }
    }
}
