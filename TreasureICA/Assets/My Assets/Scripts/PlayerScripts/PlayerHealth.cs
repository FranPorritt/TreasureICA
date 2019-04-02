using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private float maxHealth = 100.0f;
    [SerializeField]
    public float playerHealth = 100.0f;

    private bool isDead = false;

    void Start()
    {

    }

    void Update()
    {
        if (playerHealth <= 0)
        {
            isDead = true;
            Debug.Log("Dead");
        }
    }
}
