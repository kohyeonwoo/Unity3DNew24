using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject nextStagePanel;

    public GameObject endGamePannel;

    public GameObject stage1Objects;

    public GameObject stage2Objects;
 
    private bool isPause;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    private void Start()
    {
        isPause = false;
    }

    public void ActiveNextStagePanel()
    {
        Pause();
        nextStagePanel.SetActive(true);
    }

    public void ActiveEndGamePanel()
    {
        Pause();
        endGamePannel.SetActive(true);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("StartAndMenuScene");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void Pause()
    {
        if (isPause == false)
        {
            Time.timeScale = 0;
            isPause = true;
            return;
        }
    }

    public void DePause()
    {
        if (isPause == true)
        {
            Time.timeScale = 1;
            isPause = false;
            return;
        }
    }

  
}
