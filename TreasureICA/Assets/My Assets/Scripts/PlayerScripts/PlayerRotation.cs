using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    private PlayerController playerController;
    [SerializeField]
    private GameObject player;

    private bool isRotating = false;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isRotating)
        {
            transform.rotation = Quaternion.LookRotation(playerController.GetLastMovement());
            Debug.Log(playerController.GetLastMovement());
        }
    }

    public void Rotate()
    {
        isRotating = true;
        transform.rotation = Quaternion.LookRotation(playerController.GetMovement());
        isRotating = false;
    }

    public void StopRotate()
    {
        transform.rotation = Quaternion.LookRotation(playerController.GetLastMovement());
    }
}
