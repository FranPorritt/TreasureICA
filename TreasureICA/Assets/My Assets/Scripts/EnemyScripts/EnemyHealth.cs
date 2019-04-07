using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    private float maxHealth = 100.0f;
    [SerializeField]
    public float enemyHealth = 100.0f;

    private bool isDead = false;
	
	// Update is called once per frame
	void Update ()
    {
        if (enemyHealth <= 0)
        {
            isDead = true;
            Debug.Log("Enemy dead");
        }

        if (isDead)
        {
            DestroyGameObject();
        }
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
