using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{    private enum GameState
    {
        Playing,
        Paused,
    }

    private GameState m_CurrentState;

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private LevelChanger levelChanger;

    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject inventoryUI;
    [SerializeField]
    private GameObject pauseUI;   
    
    public bool gameOver = false;
    public bool restartLevel = false;
    public bool returnMenu = false;
    public bool isQuit = false;

    void Start()
    {
        gameOverUI.SetActive(false);
        inventoryUI.SetActive(true);
        pauseUI.SetActive(false);
        m_CurrentState = GameState.Playing;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerHealth.isDead)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_CurrentState == GameState.Playing)
            {
                m_CurrentState = GameState.Paused;
            }
            else
            {
                m_CurrentState = GameState.Playing;
            }
        }

        switch (m_CurrentState)
        {
            case GameState.Playing:
                {
                    break;
                }

            case GameState.Paused:
                {
                    Pause();
                    break;
                }

            default:
                {
                    break;
                }
        }
	}

    public void Pause()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        m_CurrentState = GameState.Playing;
    }

    void GameOver()
    {        
        gameOver = true;
        cameraController.GameOver();
        gameOverUI.SetActive(true);
        inventoryUI.SetActive(false);
        playerController.enabled = false;
    }
        
    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        m_CurrentState = GameState.Playing;
        returnMenu = true;
        levelChanger.FadeToLevel();
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        m_CurrentState = GameState.Playing;
        isQuit = true;
        levelChanger.FadeToLevel();
    }
}
