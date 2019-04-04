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

    private Rigidbody rigidBody;
    [SerializeField]
    private Camera playerCamera;
    private CameraController cameraController;
    private PlayerRotation playerRotation;

    // Movement
    Vector3 movement;
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float sprintBoost = 0.5f;
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
        rigidBody = GetComponent<Rigidbody>();
        cameraController = playerCamera.GetComponent<CameraController>();
        playerRotation = GetComponentInChildren<PlayerRotation>();
        currentState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    Debug.Log("Sprint");

        //    moveVertical *= Time.deltaTime + sprintBoost;
        //    moveHorizontal *= Time.deltaTime + sprintBoost;

        //    movement = new Vector3(moveHorizontal, 0, moveVertical);

        //    transform.Translate(movement);
        //}

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

        if (isSkull)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Loading scene");
                //SceneManager.LoadScene("Level 1b");
            }
        }
    }

    void Movement()
    {
        Quaternion playerRotation = transform.rotation;

        if (Input.GetKey(KeyCode.W))
        {
            float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(0, 0, moveVertical);
            currentState = State.Moving;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(0, 0, -moveVertical);
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
            Debug.Log("Press ENTER to continue");
            isSkull = true;
            Debug.Log(isSkull);
            //if (Input.GetKey(KeyCode.KeypadEnter))
            //{
            //    //Load level 1b
            //    Debug.Log("Loading scene");
            //    SceneManager.LoadScene("Level 1b");
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CameraEdge")
        {
            isPlayerAtEdge = false;
            //cameraController.ResetRotation();
        }

        if (other.tag == "SkullStick")
        {
            isSkull = false;
            Debug.Log(isSkull);
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
