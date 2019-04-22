using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MenuEnemyController : MonoBehaviour
{
    private enum State
    {
        Wandering,
    };

    private State m_CurrentState;

    [SerializeField]
    private NavMeshAgent agent;
    private GameObject enemy;

    private float m_MapExtentX = 100.0f;
    private float m_MapExtentZ = 50.0f;

    private Transform startTransform;
    private Vector3 enemyPos;

    void Start()
    {
        enemy = GetComponent<GameObject>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        m_CurrentState = State.Wandering;
        RandomPosition();
    }

    void Update()
    {

        switch (m_CurrentState)
        {
            case State.Wandering:
                {
                    Wandering();
                    break;
                }

            default:
                {
                    break;
                }
        }
    }

    private void Wandering()
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
