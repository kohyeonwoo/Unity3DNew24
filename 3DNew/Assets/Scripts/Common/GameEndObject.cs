using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.Instance.ActiveNextStagePanel();
            Debug.Log("스테이지 클리어");
        }
    }
}
