using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDamage : MonoBehaviour
{
    public int damage;
    public bool _attack;

    [Header("connect script")]
    public List<EnemyController> enemyCollider = new List<EnemyController>();

    [Header("attack time")]
    public float secondsLeft;
    [SerializeField] private float lastSecondLeft;

    void Start()
    {
        lastSecondLeft = secondsLeft;
    }

    void Update()
    {
        if (_attack)
        {
            lastSecondLeft -= Time.deltaTime;
            if (lastSecondLeft <= 0)
            {
                foreach (var _enemy in enemyCollider)
                {
                    _enemy.Hit(damage);
                }

                lastSecondLeft = secondsLeft;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();

            _attack = true;

            if (enemy != null && !enemyCollider.Contains(enemy))
            {
                enemyCollider.Add(enemy);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy != null && enemyCollider.Contains(enemy))
            {
                enemyCollider.Remove(enemy);
            }
            _attack = false;
        }
    }

}
