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
    public PlayerHealth playerHealth;
    [SerializeField]
    private Animator animator;
    
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

    // Level Change
    [SerializeField]
    private GameObject skullStick;
    private SkullLevelChange skullStickScript;

    // Lantern 
    private LanternPickUp lantern;
    [SerializeField]
    private GameObject Lantern;
    private bool inLanternRange = false;

    // Lever + Bridge
    [SerializeField]
    private Lever lever;
    public bool atLever = false;

    public bool hasMap = false;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        rigidBody = GetComponent<Rigidbody>();
        cameraController = playerCamera.GetComponent<CameraController>();
        playerRotation = GetComponentInChildren<PlayerRotation>();
        playerHealth = GetComponent<PlayerHealth>();
        skullStickScript = skullStick.GetComponent<SkullLevelChange>();
        Lantern.SetActive(false);
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

        if (inLanternRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                LanternPickUp();
            }
        }

        if (atLever)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                lever.isLeverPulled = true;
            }
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
        if (other.CompareTag("CameraEdge"))
        {
            isPlayerAtEdge = true;
        }

        if (other.CompareTag("SkullStick"))
        {
            skullStickScript.isSkullActive = true;
        }

        if (other.CompareTag("Enemy"))
        {
            playerHealth.takeDamage();
        }

        if (other.CompareTag("Stairs"))
        {
            cameraController.isPlayerOnStairs = true;
        }

        if (other.CompareTag("LanternPickUp"))
        {           
            lantern = other.GetComponent<LanternPickUp>();
            lantern.isInRange = true;
            inLanternRange = true;
        }

        if (other.CompareTag("Lever"))
        {
            atLever = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CameraEdge"))
        {
            isPlayerAtEdge = false;
        }

        if (other.CompareTag("SkullStick"))
        {
            skullStickScript.isSkullActive = false;
        }

        if (other.CompareTag("Stairs"))
        {
            cameraController.isPlayerOnStairs = false;
        }

        if (other.CompareTag("LanternPickUp"))
        {
            lantern = other.GetComponent<LanternPickUp>();
            lantern.isInRange = false;
            inLanternRange = false;
        }
    }

    void LanternPickUp()
    {        
        lantern.isPickedUp = true;
        Lantern.SetActive(true);
    }

    public Vector3 GetMovement()
    {
        return movement;
    }
    public Vector3 GetLastMovement()
    {
        return lastMovement;
    }

   // onDest
}
