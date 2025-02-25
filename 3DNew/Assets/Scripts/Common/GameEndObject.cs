using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.Instance.ActiveEndGamePanel();
            Debug.Log("게임 종료");
        }
    }
}
