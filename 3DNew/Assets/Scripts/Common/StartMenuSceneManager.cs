using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenuSceneManager : MonoBehaviour
{

    public void LoadInGameScene()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void SetLevel(int Level)
    {
        DataController.Instance.gameData.stageLevel = Level;
        DataController.Instance.SaveGameData();
    }


}
