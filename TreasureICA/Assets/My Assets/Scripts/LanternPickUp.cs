using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternPickUp : MonoBehaviour {

    public bool isInRange = false;
    public bool isPickedUp = false;

    [SerializeField]
    private GameObject LanternTextPrefab;

    // Use this for initialization
    void Start ()
    {
        LanternTextPrefab.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isInRange)
        {
            LanternTextPrefab.SetActive(true);
        }
        else
        {
            LanternTextPrefab.SetActive(false);
        }

		if (isPickedUp)
        {
            Destroy(gameObject);
        }
	}
}
