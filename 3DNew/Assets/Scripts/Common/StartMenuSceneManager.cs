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

}
