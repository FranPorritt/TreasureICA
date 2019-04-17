using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    private EnemyController enemy;

    private float maxHealth = 100.0f;
    [SerializeField]
    public float enemyHealth = 100.0f;

    private bool isDead = false;

    // Health Bar UI
    public Image healthBar;

    private void Start()
    {
        enemy = GetComponent<EnemyController>();
        healthBar.enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if (enemyHealth <= 0)
        {
            isDead = true;
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
        // ADD DIFFERENT AMOUNTS FOR DIFFERENT WEAPONS

        enemyHealth -= 25.0f;
        healthBar.fillAmount = enemyHealth / maxHealth;
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
