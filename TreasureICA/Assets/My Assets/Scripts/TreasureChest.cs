using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private bool hasKey = false;

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject openText;
    [SerializeField]
    private GameObject keyText;

    // Sound
    [SerializeField]
    private AudioClip openingSound;
    private AudioSource audioSource;
    private GameObject music;
    [SerializeField]
    private GameObject endMusic;

    [SerializeField]
    private LevelChanger levelChanger;

    public bool gameEnd = false;

    void Start()
    {
        openText.SetActive(false);
        keyText.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        music = GameObject.FindGameObjectWithTag("Music");
        endMusic.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
		if (hasKey)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameEnd = true;
                Destroy(music);
                audioSource.PlayOneShot(openingSound);
                endMusic.SetActive(true);
                animator.SetTrigger("OpenChest");
            }
        }
	}

    bool CheckForKey()
    {
        if (player.playerHasKey)
        {
            hasKey = true;
        }

        return hasKey;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckForKey();
            if (hasKey)
            {
                openText.SetActive(true);
            }
            else
            {
                keyText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        openText.SetActive(false);
        keyText.SetActive(false);
    }
}
