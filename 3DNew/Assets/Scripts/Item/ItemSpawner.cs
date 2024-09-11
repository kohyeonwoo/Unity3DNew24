using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<DNA> spawnDna = new List<DNA>();

    public float breakTime = 2.0f;
    private float countDown = 1.0f;
    private int waveCount = 0;

    private void Update()
    {
        if (countDown <= 0.0f)
        {

            countDown = breakTime;
        }

        countDown -= Time.deltaTime;
    }

    public void Spawn()
    {
        GameObject item = ObjectPoolingManager.Instance.GetPooledObject();

       // int index = Random.Range(0);

        if(item != null)
        {
            //item.transform.position;
            item.SetActive(true);
        }

    }

}
