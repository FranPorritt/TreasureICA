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

    [SerializeField]
    private NavMeshAgent agent;
    private GameObject enemy;

    private float m_MapExtentX = 50.0f;
    private float m_MapExtentZ = 50.0f;

    private Transform startTransform;

    void Start ()
    {
        enemy = GetComponent<GameObject>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        RandomPosition();
    }
	
	void Update ()
    {
        if (agent.remainingDistance < 2.0f)
        {
            RandomPosition();
        }
    }

    private void RandomPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-m_MapExtentX, m_MapExtentX), Random.Range(0f, 0f), Random.Range(-m_MapExtentZ, m_MapExtentZ));
        agent.SetDestination(randomPosition);
    }
}
