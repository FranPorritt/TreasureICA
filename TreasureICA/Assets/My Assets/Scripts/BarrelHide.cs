using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHide : MonoBehaviour
{
    private bool isBarrelActive = false;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameObject interactText;

    // Use this for initialization
    void Start ()
    {
        player.SetActive(true);
        interactText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(isBarrelActive)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                playerController.isPlayerHidden = !playerController.isPlayerHidden;
            }
        }

        if(playerController.isPlayerHidden)
        {
            player.SetActive(false);
        }
        else
        {
            player.SetActive(true);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isBarrelActive = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isBarrelActive = false;
            interactText.SetActive(true);
        }
    }
}
