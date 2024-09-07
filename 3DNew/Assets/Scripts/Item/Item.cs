using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { DNA, Coin,}

public class Item : MonoBehaviour
{
    public ItemType itemTypes;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Debug.Log("������ ���� �� �÷��̾� ���� ��ġ ����");
        }
    }
}
