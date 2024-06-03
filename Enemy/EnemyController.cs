using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Animator EnemyAnim;

    [Header("Connect levelManager")]
    private LevelManager levelManager;

    [Header("Health")]
    public Slider healthBarPrefab;
    Slider healthBar;
    public int maxHealth;

    [Header("EnemyWallet")]
    public int EnemyWallet;
    public Money money;

    private List<Transform> wayPoints;

    private float agentStoppingDistance = 0.3f;

    private bool wayPointSet = false;

    private int currentWayPointIndex = 0;

    NavMeshAgent agent;

    void Start()
    {
        EnemyAnim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        levelManager = FindObjectOfType<LevelManager>();
        money = FindObjectOfType<Money>();

        healthBar = Instantiate(healthBarPrefab, this.transform.position, Quaternion.identity);
        healthBar.transform.SetParent(GameObject.Find("EnemyHealth").transform);
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    void Update()
    {
        if (!wayPointSet)
        {
            return;
        }

        if (healthBar)
        {
            healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up * 2f);
        }

        if(!agent.pathPending && agent.remainingDistance <= agentStoppingDistance)
        {
            if(currentWayPointIndex == wayPoints.Count)
            {
                levelManager.EnemyDestroyed();
                Destroy(this.gameObject);
                Destroy(healthBar.gameObject);
            }
            else
            {
                agent.SetDestination(wayPoints[currentWayPointIndex].position);
                currentWayPointIndex++;
            }
        }
    }

    public void SetDestination(List<Transform> wayPoints)
    {
        this.wayPoints = wayPoints;
        wayPointSet = true;
    }

    public void Hit(int damage)
    {
        if (healthBar)
        {
            healthBar.value -= damage;
            if(healthBar.value <= 0)
            {
                float range = 15f;
                
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

                foreach (var hitCollider in hitColliders)
                {
                    Tower tower = hitCollider.GetComponent<Tower>();
                    if(tower != null)
                    {
                        tower.EnemyDestroyed(gameObject);
                    }
                }
                EnemyAnim.SetBool("die", true);
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                gameObject.GetComponent<EnemyController>().enabled = false;

                Destroy(healthBar.gameObject);
                Destroy(this.gameObject, 5);
                levelManager.EnemyDestroyed();
                money._money += EnemyWallet;
            }
        }
    }
}