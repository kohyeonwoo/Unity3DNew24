using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : Item
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
