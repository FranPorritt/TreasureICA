using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkullLevelChange : MonoBehaviour
{
    public bool isSkullActive = false;
    private int currentScene;
    [SerializeField]
    private LevelChanger levelChanger;

    [SerializeField]
    private GameObject SkullTextPrefab;
    [SerializeField]
    private GameObject GetMapText;
    private Vector3 skullStickPos;

    [SerializeField]
    private PlayerController player;
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
            if (player.playerHasMap)
            {
                SkullTextPrefab.SetActive(true);

                if (Input.GetKey(KeyCode.E))
                {
                    levelChanger.FadeToLevel();
                }
            }
            else
            {
                GetMapText.SetActive(true);
            }
        }
        else
        {
            SkullTextPrefab.SetActive(false);
            GetMapText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSkullActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSkullActive = false;
        }
    }
}
