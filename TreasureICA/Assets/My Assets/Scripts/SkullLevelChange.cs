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

            if (Input.GetKey(KeyCode.E))
            {
                levelChanger.FadeToLevel();
            }
        }
        else
        {
            SkullTextPrefab.SetActive(false);
        }
    }
}
