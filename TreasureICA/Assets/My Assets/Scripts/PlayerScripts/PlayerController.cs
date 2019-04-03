using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    private Rigidbody rigidBody;
    [SerializeField]
    private Camera playerCamera;
    private CameraController cameraController;
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float sprintBoost = 0.5f;
    [SerializeField]
    private float jumpForce = 100.0f;
    
    public bool isGrounded = true;
    public bool isPlayerAtEdge = false;

    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        cameraController = playerCamera.GetComponent<CameraController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        float moveVertical = Input.GetAxis("Vertical") * speed;
        float moveHorizontal = Input.GetAxis("Horizontal") * speed;

        moveVertical *= Time.deltaTime;
        moveHorizontal *= Time.deltaTime;

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        transform.Translate(movement);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Sprint");

            moveVertical *= Time.deltaTime + sprintBoost;
            moveHorizontal *= Time.deltaTime + sprintBoost;

            movement = new Vector3(moveHorizontal, 0, moveVertical);

            transform.Translate(movement);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CameraEdge")
        {
            isPlayerAtEdge = false;
            //cameraController.ResetRotation();
        }
    }
}
