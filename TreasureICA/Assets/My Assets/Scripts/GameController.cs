using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GameObject player;
    private PlayerController playerController;
    private PlayerHealth playerHealth;

    private bool gameOver = false;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerHealth = player.GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerHealth.isDead)
        {
            GameOver();
        }
	}

    void GameOver()
    {
        // Game over hud
    }
}
