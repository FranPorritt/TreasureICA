using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public Animator animator;
    private int currentScene;

    [SerializeField]
    private GameController game;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (game.returnMenu)
        {
            SceneManager.LoadScene(0);
        }
        else if (game.isQuit)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }
}
