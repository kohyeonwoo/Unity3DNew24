using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour 
{

    public static ObjectPoolingManager Instance;

    private List<GameObject> poolObjects = new List<GameObject>();
    private int amountToPool = 30;

    [SerializeField]
    private GameObject poolPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for(int i =0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(poolPrefab);
            obj.SetActive(false);
            poolObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
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
