using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    private enum State
    {
        Wandering,
        Chasing,
        Attacking,
    };

    private State m_CurrentState;

    [SerializeField]
    private NavMeshAgent agent;
    private GameObject enemy;

    private float m_MapExtentX = 50.0f;
    private float m_MapExtentZ = 50.0f;

    private Transform startTransform;

    private EnemyHealth enemyHealth;
    private PlayerAttack playerAttack;

    private bool isInAttackRange = false;

    void Start ()
    {
        enemy = GetComponent<GameObject>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        RandomPosition();
    }
	
	void Update ()
    {
        if (agent.remainingDistance < 2.0f)
        {
            RandomPosition();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("hit");
            enemyHealth.enemyHealth -= 10;
        }
    }

    private void RandomPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-m_MapExtentX, m_MapExtentX), Random.Range(0f, 0f), Random.Range(-m_MapExtentZ, m_MapExtentZ));
        agent.SetDestination(randomPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttack")
        {
            Debug.Log("in attack range");
            isInAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerAttack")
        {
            isInAttackRange = false;
        }
    }
}
