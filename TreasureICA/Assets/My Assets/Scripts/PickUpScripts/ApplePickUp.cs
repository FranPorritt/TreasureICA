using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePickUp : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;

    private int increaseHealthAmount = 20;

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
