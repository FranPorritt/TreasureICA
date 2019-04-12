using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Moving,
    };

    State currentState;

    private int currentScene;

    private Rigidbody rigidBody;
    [SerializeField]
    private Camera playerCamera;
    private CameraController cameraController;
    private PlayerRotation playerRotation;
    private PlayerHealth playerHealth;
    [SerializeField]
    private Animator animator;
    private Vector3 playerPos;

    // Movement
    Vector3 movement;
    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    private float sprintBoost = 0f;
    [SerializeField]
    private float jumpForce = 100.0f;
    public bool isGrounded = true;

    // Player Rotation & Camera Rotation
    public bool isPlayerAtEdge = false;
    private Vector3 lastMovement;
    public bool isMoving = false;

    private bool isSkull = false;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        rigidBody = GetComponent<Rigidbody>();
        cameraController = playerCamera.GetComponent<CameraController>();
        playerRotation = GetComponentInChildren<PlayerRotation>();
        playerHealth = GetComponent<PlayerHealth>();
        currentState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (currentState == State.Moving)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("ATTACK");
            animator.SetBool("isAttacking", true);
            animator.Play("playerSword");
            animator.SetBool("isAttacking", false);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

        if (isSkull)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Loading scene");
                SceneManager.LoadScene(currentScene + 1);
            }
        }
    }

    void Movement()
    {
        Quaternion playerRotation = transform.rotation;

        if (Input.GetKey(KeyCode.W))
        {
            float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveVertical += sprintBoost;
            }
            transform.Translate(0, 0, moveVertical);
            currentState = State.Moving;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveVertical += sprintBoost;
            }
            transform.Translate(0, 0, moveVertical);
            currentState = State.Moving;
        }
        else
        {
            currentState = State.Idle;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, playerRotation.y -= 3, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, playerRotation.y += 3, 0);
        }
    }

    void Jump()
    {
        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CameraEdge")
        {
            isPlayerAtEdge = true;
        }

        if (other.tag == "SkullStick")
        {
            isSkull = true;
            Debug.Log("Press E to continue forward!");
        }

        if (other.tag == "Enemy")
        {
            playerHealth.playerHealth -= 5.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CameraEdge")
        {
            isPlayerAtEdge = false;
        }

        if (other.tag == "SkullStick")
        {
            isSkull = false;
        }
    }

    public Vector3 GetMovement()
    {
        return movement;
    }
    public Vector3 GetLastMovement()
    {
        return lastMovement;
    }
}
