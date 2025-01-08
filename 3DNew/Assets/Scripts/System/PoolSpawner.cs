using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{

    public List<Transform> spawnLocations = new List<Transform>();

    public float spawnBreakTime = 2.5f;

    private float countDown = 1.0f;

    private int waveCount = 0;


    public void Spawn()
    {
        GameObject enemy = ObjectPool.instance.GetPoolObject();

        int i = Random.Range(0, spawnLocations.Count);

        if(enemy)
        {
            enemy.transform.position = spawnLocations[i].position;
            enemy.SetActive(true);
        }
    }

}
