using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    // When you load a new level need to get health value
    private float maxHealth = 100.0f;
    [SerializeField]
    public float playerHealth = 100.0f;

    [SerializeField]
    private float enemyDamage = 5.0f;

    public bool isDead = false;

    // Health Bar UI
    public Image healthHeart;

    void Start()
    {

    }

    void Update()
    {
        healthHeart.fillAmount = playerHealth / maxHealth;

        if (playerHealth <= 0)
        {
            isDead = true;
            Debug.Log("Dead");
        }
    }

    public void takeDamage()
    {
        playerHealth -= enemyDamage;        
    }
}
