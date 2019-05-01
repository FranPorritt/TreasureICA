using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;

    [SerializeField]
    private GameObject PickUpTextPrefab;

    private bool isColliding = false;

    // Sound
    [SerializeField]
    private PlayerController player;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if(player == null) // For key spawn
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        PickUpTextPrefab.SetActive(false);
    }

    private void Update()
    {
        // Rotates object
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);

        if (isColliding)
        {
            PickUpTextPrefab.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int inventoryIndex = 0; inventoryIndex < inventory.inventorySlots.Length; inventoryIndex++)
                {
                    if (!inventory.isFull[inventoryIndex])
                    {
                        inventory.isFull[inventoryIndex] = true;
                        Instantiate(itemButton, inventory.inventorySlots[inventoryIndex].transform, false);
                        player.PickUpSound();
                        Destroy(gameObject);
                        break;
                    }
                }
            }
        }
        else
        {
            PickUpTextPrefab.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
}
