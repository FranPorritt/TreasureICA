using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkullLevelChange : MonoBehaviour {

    private bool isSkullActive = false;
    private int currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update ()
    {
		if (isSkullActive)
        {
            if (Input.GetKey(KeyCode.E))
            {
                // WHAT IF PLAYER FALLS TO LOWER LEVEL AND/OR GOES BACK TO LAST AREA
                SceneManager.LoadScene(currentScene + 1);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isSkullActive = true;
            Debug.Log("Press E to continue forward!");
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isSkullActive = false;
        }
    }
}
