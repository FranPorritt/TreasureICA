using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public static PlayerHealth player;

    // When you load a new level need to get health value
    private float maxHealth = 100.0f;
    public float playerHealth = 100.0f;

    [SerializeField]
    private int enemyDamage = 1;
    [SerializeField]
    private int bossDamage = 5;

    public bool isDead = false;

    // Health Bar UI
    public Image healthHeart;

    private void Start()
    {
       //playerHealth = PlayerPrefs.GetFloat("Health", 100); // COMMENTED OUT WHILE TESTING -- ENABLE IT FOR PLAY
       if (playerHealth <= 0)
        {
            playerHealth = 100; // If died and restarted
        }
    }

    void Update()
    {
        healthHeart.fillAmount = playerHealth / maxHealth;

        if (playerHealth <= 0)
        {
            isDead = true;
            Debug.Log("Dead");
        }

        if (isDead)
        {
            healthHeart.enabled = false;
        }

        // Stops health going above 100
        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }
    }

    public void takeDamage()
    {
        playerHealth -= enemyDamage;        
    }

    public void takeDamageBoss()
    {
        playerHealth -= bossDamage;
    }

    // Stores player data between levels
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("Health", playerHealth);
    }
}
