using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    private GameObject heart;

    // Sound
    [SerializeField]
    private AudioClip playerDamage;
    private AudioSource audioSource;
    private bool isSoundPlaying = false;

    [SerializeField]
    private CutScene cutscene;

    private void Start()
    {
       playerHealth = PlayerPrefs.GetFloat("Health", 100); // COMMENTED OUT WHILE TESTING -- ENABLE IT FOR PLAY
       if (playerHealth <= 0 || SceneManager.GetActiveScene().name == "Level 1 - Beach")
        {
            playerHealth = 100; // If died and restarted
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (cutscene != null && cutscene.isCutscene)
        {
            heart.SetActive(false);
        }
        else if (cutscene == null || !cutscene.isCutscene)
            {
            heart.SetActive(true);
        }

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
        if (!isDead)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(playerDamage);
            }
        }
    }

    public void takeDamageBoss()
    {
        playerHealth -= bossDamage;
        if (!isDead)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(playerDamage);
            }
        }
    }

    // Stores player data between levels
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("Health", playerHealth);
    }
}
