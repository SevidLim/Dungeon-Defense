using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Character")]
    public Animator TowerAnim;
    public GameObject core;
    public GameObject Weapon;

    [Header("Attack")]
    public float turningSpeed = 10;
    public float angleTurningAccuracy = 60;

    [SerializeField] private List<GameObject> enemiesInRange = new List<GameObject>();
    private GameObject currentTarget;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackSpeed;
    public int _damage;
    public float secondsLeft;
    public float lastSecondLeft;

    [Header("TowerPlace")]
    public TowerPlace _towerPlace;

    [Header("UpgradeState")]
    public UpgradeState upgradeState;

    public Money money;
    public GameObject _money;

    void Start()
    {
        _money = GameObject.Find("Money");
        if (_money != null)
        {
            money = _money.GetComponent<Money>();
        }

        lastSecondLeft = secondsLeft;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TowerAnim.SetBool("attack", true);
            enemiesInRange.Add(other.gameObject);
            UpdateTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TowerAnim.SetBool("attack", false);
            enemiesInRange.Remove(other.gameObject);
            currentTarget = null;
            UpdateTarget();
        }
    }

    private void UpdateTarget()
    {
        if (currentTarget != null)
        {
            return;
        }

        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach(GameObject enemy in enemiesInRange)
        {
            if(enemy == null) 
            {
                return;
            }

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if(closestEnemy != null)
        {
            currentTarget = closestEnemy;
        }
        else
        {
            currentTarget = null;
        }
    }

    void Update()
    {
        if (currentTarget != null)
        {
            Vector3 aimAt = new Vector3(currentTarget.transform.position.x, core.transform.position.y, currentTarget.transform.position.z);
            float disToTarget = Vector3.Distance(aimAt, Weapon.transform.position);

            Vector3 relativeTargetPosition = Weapon.transform.position + (Weapon.transform.forward * disToTarget);

            relativeTargetPosition = new Vector3(relativeTargetPosition.x, currentTarget.transform.position.y, relativeTargetPosition.z);

            Weapon.transform.rotation = Quaternion.Slerp(Weapon.transform.rotation, Quaternion.LookRotation(relativeTargetPosition - Weapon.transform.position), Time.deltaTime * turningSpeed);
            core.transform.rotation = Quaternion.Slerp(core.transform.rotation, Quaternion.LookRotation(aimAt - core.transform.position), Time.deltaTime * turningSpeed);

            Vector3 directionToTarget = currentTarget.transform.position - Weapon.transform.position;

            if(Vector3.Angle(directionToTarget, Weapon.transform.forward) < angleTurningAccuracy)
            {
                lastSecondLeft -= Time.deltaTime;
                if (lastSecondLeft <= 0)
                {
                    Fire();
                }
            }
        }
        else
        {

            lastSecondLeft = secondsLeft;
            UpdateTarget();
        }
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        if (enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
            UpdateTarget();
        }
    }

    public void sellTower()
    {
        Destroy(gameObject);
        _towerPlace.IsPlacing = false;
        money._money += upgradeState.totalspend;
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetDamage(_damage);
        projectile.GetComponent<Rigidbody>().velocity = firePoint.forward * attackSpeed;
        lastSecondLeft = secondsLeft;
    }
}