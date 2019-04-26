using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i;
	
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

	// Update is called once per frame
	void Update ()
    {
		if (transform.childCount <= 1)
        {
            inventory.isFull[i] = false;
        }
	}
}
