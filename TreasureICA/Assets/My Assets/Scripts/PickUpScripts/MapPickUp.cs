﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPickUp : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerController.playerHasMap = true;
    }
}
