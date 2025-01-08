using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public List<GameObject> enemyPrefabs = new List<GameObject>();

    private List<GameObject> poolObjects = new List<GameObject>();

    [SerializeField]
    private int amountToPool = 15;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for(int i =0; i <amountToPool; i++)
        {
            GameObject obj = Instantiate(enemyPrefabs[0]);
            obj.SetActive(false);
            poolObjects.Add(obj);
        }
    }

    public GameObject GetPoolObject()
    {
        for(int i =0; i < poolObjects.Count; i++)
        {
            if(!poolObjects[i].activeInHierarchy)
            {
                return poolObjects[i];
            }
        }
        return null;
    }

}
