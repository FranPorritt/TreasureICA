using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePickUp : MonoBehaviour {

    private PlayerHealth playerHealth;

    private float increaseHealthAmount = 20.0f;

	// Use this for initialization
	void Start ()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}

    public void Use()
    {
        playerHealth.playerHealth += increaseHealthAmount;
        Destroy(gameObject);
    }
}
