using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkullLevelChange : MonoBehaviour
{

    public bool isSkullActive = false;
    private int currentScene;

    [SerializeField]
    private GameObject SkullTextPrefab;
    private Vector3 skullStickPos;

    private bool isTextActive = false;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        skullStickPos = this.transform.position;
        SkullTextPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSkullActive)
        {
            SkullTextPrefab.SetActive(true);

            //if ((SkullTextPrefab) && (!isTextActive))
            //{
            //    ShowSkullText();
            //}

            if (Input.GetKey(KeyCode.E))
            {
                // WHAT IF PLAYER FALLS TO LOWER LEVEL AND/OR GOES BACK TO LAST AREA
                SceneManager.LoadScene(currentScene + 1);
            }
        }
        else
        {
            SkullTextPrefab.SetActive(false);
        }
    }

    //private void ShowSkullText()
    //{
    //    Instantiate(SkullTextPrefab, skullStickPos, Quaternion.identity, transform);
    //    isTextActive = true;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        isSkullActive = true;
    //        Debug.Log("Press E to continue forward!");
    //    }        
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        isSkullActive = false;
    //    }
    //}
}
