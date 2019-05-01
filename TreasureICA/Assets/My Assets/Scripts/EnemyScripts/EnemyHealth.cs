using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    private EnemyController enemy;
    [SerializeField]
    private GameObject Boss;

    private float maxHealth = 100.0f;
    [SerializeField]
    public float enemyHealth = 100.0f;

    private bool isDead = false;

    [SerializeField]
    private GameObject keyDrop;

    // Health Bar UI
    public Image healthBar;

    // Sound
    [SerializeField]
    private AudioClip enemyHit;
    private AudioSource audioSource;

    private void Start()
    {
        enemy = GetComponent<EnemyController>();
        healthBar.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (enemyHealth <= 0)
        {
            isDead = true;
        }

        if (this.CompareTag("Boss"))
        {
            healthBar.enabled = true;
        }

        if (isDead)
        {
            DestroyGameObject();
        }

        if (enemy.distance < 10.0f)
        {
            healthBar.enabled = true;
        }
        else
        {
            healthBar.enabled = false;
        }
    }

    public void TakeDamage()
    {
        enemyHealth -= 25.0f;
        healthBar.fillAmount = enemyHealth / maxHealth;
        audioSource.PlayOneShot(enemyHit);
    }
    public void TakeDamageBoss()
    {
        enemyHealth -= 10.0f;
        healthBar.fillAmount = enemyHealth / maxHealth;
        audioSource.PlayOneShot(enemyHit);
    }

    void DestroyGameObject()
    {
        if (this.CompareTag("Boss"))
        {
            Instantiate(keyDrop, Boss.transform, false);
        }
        Destroy(gameObject);
    }
}
