using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuSceneManager : MonoBehaviour
{
    [SerializeField]
    private bool bOpen = false;

    public GameObject startButton;

    private void Update()
    {
        if(bOpen == true)
        {
            startButton.SetActive(false);
        }
    }

    public void OpenStartScene()
    {
        bOpen = true;
    }

}