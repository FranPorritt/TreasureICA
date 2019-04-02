using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;

    [SerializeField]
    private float cameraDistance;
    [SerializeField]
    private GameObject player;
    private Camera playerCamera;

    [SerializeField]
    private float rotateSpeed = 0.5f;

    void Start()
    {
        playerCamera = GetComponent<Camera>();
        Vector3 playerPos = player.transform.position;
        offset = new Vector3(playerPos.x, playerPos.y + cameraDistance, playerPos.z - (cameraDistance - 1));
        playerCamera.transform.position = playerPos + offset;
        transform.LookAt(playerPos);        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        playerCamera.transform.position = playerPos + offset;

        if (playerPos.x <= -10) //TO DO: INCREMENT AS PLAYER MOVES NOT CONTINOUSLY
        {
            offset = Quaternion.AngleAxis(rotateSpeed, Vector3.up) * offset;
            transform.position = playerPos + offset;
            transform.LookAt(playerPos);
        }
    }
}
