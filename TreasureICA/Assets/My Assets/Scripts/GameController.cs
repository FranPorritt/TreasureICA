using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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
    private CutScene cutscene;
    [SerializeField]
    private TreasureChest treasure;
    [SerializeField]
    private GameObject endGame;
    [SerializeField]
    private PlayableDirector endCutScene;

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
    private bool endingGame = false;

    void Start()
    {
        gameOverUI.SetActive(false);
        inventoryUI.SetActive(true);
        pauseUI.SetActive(false);
        endGame.SetActive(false);
        m_CurrentState = GameState.Playing;
    }
	
	void Update ()
    {
        if (playerHealth.isDead)
        {
            GameOver();
        }

        // Pause Menu
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

        // State machine
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

        if(treasure.gameEnd)
        {
            if (!endingGame)
            {
                EndGame();
            }
            else if (endCutScene.state != PlayState.Playing)
            {
                levelChanger.FadeToLevel();
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

    private void EndGame()
    {
        endingGame = true;
        endGame.SetActive(true);
        endCutScene.Play();
    }
}
