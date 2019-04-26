using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;

    public bool[] isFull;
    public GameObject[] inventorySlots;
    public string[] inventoryItem;      

    void Awake()
    {
        if (inventory == null)
        {
            DontDestroyOnLoad(gameObject);
            inventory = this;
        }
        else if (inventory != this)
        {
            Destroy(gameObject);
        }

        //for (int inventoryIndex = 0; inventoryIndex < inventorySlots.Length; inventoryIndex++)
        //{
        //    if (inventory.isFull[inventoryIndex])
        //    {
                
        //    }
        //}
    }
}
