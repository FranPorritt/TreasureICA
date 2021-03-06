﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;

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
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
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
    
    // Lantern 
    private LanternPickUp lantern;
    [SerializeField]
    private GameObject Lantern;
    private bool inLanternRange = false;

    // Lever + Bridge
    [SerializeField]
    private Lever lever;
    public bool atLever = false;

    // Pick Ups
    public bool playerHasKey = false;
    public bool playerHasMap = false;
    public bool isPlayerHidden = false;

    // Sound
    [SerializeField]
    private AudioClip playerSwing;
    [SerializeField]
    private AudioClip pickUpSound;
    private AudioSource audioSource;

    [SerializeField]
    private CutScene cutscene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        rigidBody = GetComponent<Rigidbody>();
        Lantern.SetActive(false);
        currentState = State.Idle;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cutscene == null || !cutscene.isCutscene)
        {
            Movement();
        }

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
            animator.SetBool("isAttacking", true);
            animator.Play("playerSword");
            animator.SetBool("isAttacking", false);

            audioSource.PlayOneShot(playerSwing);
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

    public void PickUpSound()
    {
        audioSource.PlayOneShot(pickUpSound);
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
