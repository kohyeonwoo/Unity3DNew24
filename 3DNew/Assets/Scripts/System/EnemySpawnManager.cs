using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    //생성될 적 관련 리스트 변수
    public List<GameObject> enemyList = new List<GameObject>();

    //적 오브젝트 풀링 리스트
    private List<GameObject> enemyPoolObject = new List<GameObject>();

    //적 몬스터 생성 위치 관련 리스트 변수
    public List<Transform> enemySpawnLocations = new List<Transform>();

    //적 생성 시간 딜레이 변수
    [SerializeField]
    private float spawnBreakTime = 2.5f;

    //적 공격 시간 딜레이 변수
    [SerializeField]
    private float countDown = 1.0f;

    //웨이브 수
    public int waveCount = 3;

    //오브젝트 풀에서 생성될 제한 수 변수
    public int amountToPool = 5;
    ////////////////////////////////////////////////

    private void Start()
    {
        CreateOriginPool();
    }

    private void Update()
    {
 
        if (countDown <= 0.0f)
        {
            StartCoroutine(SpawnOrigin());
            countDown = spawnBreakTime;
        }

        countDown -= Time.deltaTime;

    }

    IEnumerator SpawnOrigin()
    {

        for (int i = 0; i < waveCount; i++)
        {
            SpawnOriginMode();

            yield return new WaitForSeconds(0.5f);
        }

    }

    public void CreateOriginPool()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(enemyList[0]);
            obj.SetActive(false);
            enemyPoolObject.Add(obj);
        }
    }

    public GameObject GetOriginPoolObject()
    {

        for (int i = 0; i < enemyPoolObject.Count; i++)
        {
            if (!enemyPoolObject[i].activeInHierarchy)
            {
                return enemyPoolObject[i];
            }
        }

        return null;

    }

    private void SpawnOriginMode()
    {

        GameObject objects = GetOriginPoolObject();

        int i = Random.Range(0, enemySpawnLocations.Count);

        if (objects != null)
        {
            objects.transform.position = enemySpawnLocations[i].position;
            objects.SetActive(true);
        }

    }

}
