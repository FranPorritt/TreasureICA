using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 lastPlayerPos;

    [SerializeField]
    private float cameraDistance;
    [SerializeField]
    private GameObject player;
    private Camera playerCamera;
    private PlayerController playerController;

    [SerializeField]
    private float rotateSpeed = 0f;
    private Quaternion startRotation;
    private bool isPlayerMoving = false;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        playerCamera = GetComponent<Camera>();
        Vector3 playerPos = player.transform.position;
        offset = new Vector3(playerPos.x, playerPos.y + cameraDistance, playerPos.z - (cameraDistance - 1));
        playerCamera.transform.position = playerPos + offset;
        transform.LookAt(playerPos);

        startRotation = playerCamera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        playerCamera.transform.position = playerPos + offset;

        Vector3 direction = playerPos - lastPlayerPos;
        Vector3 localDirection = transform.InverseTransformDirection(direction);
        Debug.Log(localDirection);

        if (player.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            isPlayerMoving = true;
        }
        else
        {
            isPlayerMoving = false;
        }

        if ((isPlayerMoving) && (playerController.isPlayerAtEdge))
        {
            Rotate(playerPos, localDirection);
        }

        lastPlayerPos = playerPos;
    }

    private void Rotate(Vector3 playerPos, Vector3 localDirection)
    {
        if (localDirection.x < 0) //Moving left
        {
            offset = Quaternion.AngleAxis(rotateSpeed, Vector3.up) * offset;
            transform.position = playerPos + offset;
            transform.LookAt(playerPos);
        }
        else if (localDirection.x > 0) //Moving right
        {
            offset = Quaternion.AngleAxis(rotateSpeed, -Vector3.up) * offset;
            transform.position = playerPos + offset;
            transform.LookAt(playerPos);
        }
    }

    public void ResetRotation()
    {
        Vector3 playerPos = player.transform.position;
        offset = new Vector3(playerPos.x, playerPos.y + cameraDistance, playerPos.z - (cameraDistance - 1));
        transform.rotation = startRotation;
        transform.LookAt(playerPos);
    }
}
