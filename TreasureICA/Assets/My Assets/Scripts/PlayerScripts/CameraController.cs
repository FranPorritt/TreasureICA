using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private float rotateBorderStart = -5f;
    [SerializeField]
    private float rotateBorderEnd = -15f;
    private Quaternion startRotation;
    private bool isPlayerMoving = false;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        playerCamera = GetComponent<Camera>();
        Vector3 playerPos = player.transform.position;
        offset = new Vector3(playerPos.x, playerPos.y + cameraDistance, playerPos.z - (cameraDistance + 1));
        playerCamera.transform.position = offset;
        transform.LookAt(playerPos);

        startRotation = playerCamera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        offset = new Vector3(playerPos.x, playerPos.y + cameraDistance, playerPos.z - (cameraDistance + 1));
        playerCamera.transform.position = offset;

        //Vector3 direction = playerPos - lastPlayerPos;
        //Vector3 localDirection = transform.InverseTransformDirection(direction);

        //if ((playerController.isMoving) && (playerPos.x <= rotateBorderStart) && (playerPos.x >= rotateBorderEnd))
        //{
        //    Rotate(playerPos, localDirection);
        //}

        //lastPlayerPos = playerPos;
    }

    private void Rotate(Vector3 playerPos, Vector3 localDirection)
    {
        if ((playerCamera.transform.position.y < 90) && (playerCamera.transform.position.y >= 0))
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
    }
}
