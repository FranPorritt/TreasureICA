using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPickUp : MonoBehaviour
{
    private PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.hasMap = true;
    }
}
