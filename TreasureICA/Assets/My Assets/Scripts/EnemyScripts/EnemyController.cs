using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
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
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private GameController game;
    [SerializeField]
    private CutScene cutscene;

    public float distance;
    private float m_MapExtentX = 100.0f;
    private float m_MapExtentZ = 50.0f;

    private Transform startTransform;
    private Vector3 enemyPos;
    [SerializeField]
    private float knockBackDistance = 2.0f;

    private EnemyHealth enemyHealth;
    private bool isInAttackRange = false;

    void Start()
    {
        enemy = GetComponent<GameObject>();
        enemyHealth = GetComponent<EnemyHealth>();
        m_CurrentState = State.Wandering;
        RandomPosition();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 3 - Treasure Cave")
        {
            if (cutscene.isCutscene)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }

        if (game.gameOver)
        {
            m_CurrentState = State.Wandering;
        }

        distance = Vector3.Distance(Player.position, this.transform.position);

        // Player Attack
        if ((Input.GetMouseButtonDown(0)) && (isInAttackRange))
        {
            enemyHealth.TakeDamage();
            KnockBack();
        }

        if(playerController.isPlayerHidden)
        {
            m_CurrentState = State.Wandering;
        }

        if ((distance < 20.0f) && (!playerController.isPlayerHidden) && (cutscene == null || !cutscene.isCutscene)) // Prevents enemies attacking player when hidden and during cutscene
        {
            m_CurrentState = State.Chasing;
        }
        else
        {
            m_CurrentState = State.Wandering;
        }

        switch (m_CurrentState)
        {
            case State.Wandering:
                {
                    Wandering();
                    break;
                }

            case State.Chasing:
                {
                    Chasing();
                    break;
                }

            case State.Attacking:
                {
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
    private void Chasing()
    {
        Vector3 PlayerPos = Player.position;
        Vector3 direction = Player.position - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        agent.SetDestination(PlayerPos);
    }

    private void RandomPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-m_MapExtentX, m_MapExtentX), Random.Range(0f, 0f), Random.Range(-m_MapExtentZ, m_MapExtentZ));
        agent.SetDestination(randomPosition);
    }

    public void KnockBack()
    {
        agent.velocity = -agent.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Debug.Log("in attack range");
            isInAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            isInAttackRange = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerHealth.takeDamage();
        }
    }
}
