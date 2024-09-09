using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject evolutionPannel;

    private bool isPause;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        isPause = false;
    }

    public void ActiveEvolutionPannel()
    {
        evolutionPannel.SetActive(true);
        
        if(isPause == false)
        {
            Time.timeScale = 0;
            isPause = true;
            return;
        }
    }

    public void DeActiveEvolutionPannel()
    {
        evolutionPannel.SetActive(false);

        if (isPause == true)
        {
            Time.timeScale = 1;
            isPause = false;
            return;
        }
    }



}
