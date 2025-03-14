using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float timer;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * 6 * Time.deltaTime);
        transform.localScale += new Vector3(3, 3, 3) * Time.deltaTime;

        timer += 1 * Time.deltaTime;

        if(timer > 1.0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            gameObject.SetActive(false);
            timer = 0;
        }
    }
}
