using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private LevelChanger levelChanger;

    private GameObject endMusic;

    private void Start()
    {
        endMusic = GameObject.FindGameObjectWithTag("EndMusic");
        Destroy(endMusic);
    }
	
	public void StartGame()
    {
        levelChanger.FadeToLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
