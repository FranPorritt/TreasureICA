using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private bool hasKey = false;

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private Animator animator;
	
	// Update is called once per frame
	void Update ()
    {
		if (hasKey)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("OpenChest");
            }
        }
	}

    bool CheckForKey()
    {
        if (player.playerHasKey)
        {
            hasKey = true;
        }

        return hasKey;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckForKey();
        }
    }
}
