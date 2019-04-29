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
    private GameObject skullCamera;
    [SerializeField]
    private CameraController playerCamera;

    [SerializeField]
    private GameObject SkullTextPrefab;
    [SerializeField]
    private GameObject GetMapText;
    private Vector3 skullStickPos;
    [SerializeField]
    private GameObject skullCanvas;
    [SerializeField]
    private GameObject inventoryCanvas;

    [SerializeField]
    private PlayerController player;
    private bool isTextActive = false;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        skullStickPos = this.transform.position;
        SkullTextPrefab.SetActive(false);
        skullCamera.SetActive(false);
        playerCamera.enabled = true;
        skullCanvas.SetActive(false);
        inventoryCanvas.SetActive(true);
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
                    SkullContinue();
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

    private void SkullCameraControl()
    {
        skullCamera.SetActive(true);
        playerCamera.enabled = false;
        skullCanvas.SetActive(true);
        inventoryCanvas.SetActive(false);
        Time.timeScale = 0;
    }

    public void SkullContinue()
    {
        skullCamera.SetActive(false);
        playerCamera.enabled = true;
        skullCanvas.SetActive(false);
        inventoryCanvas.SetActive(true);
        Time.timeScale = 1.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSkullActive = true;

            // Camera Effect
            SkullCameraControl();
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
