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

    [SerializeField]
    private GameObject openText;
    [SerializeField]
    private GameObject keyText;

    [SerializeField]
    private LevelChanger levelChanger;

    void Start()
    {
        openText.SetActive(false);
        keyText.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
		if (hasKey)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("OpenChest");
                levelChanger.FadeToLevel();
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
            if (hasKey)
            {
                openText.SetActive(true);
            }
            else
            {
                keyText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        openText.SetActive(false);
        keyText.SetActive(false);
    }
}
