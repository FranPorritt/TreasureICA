using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private LevelChanger levelChanger;
	
	public void StartGame()
    {
        levelChanger.FadeToLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
