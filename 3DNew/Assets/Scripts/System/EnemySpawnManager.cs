using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    //생성될 적 관련 리스트 변수
    public List<GameObject> enemyList = new List<GameObject>();

    //적 오브젝트 풀링 리스트
    private List<GameObject> enemyPoolObject = new List<GameObject>();
    private List<GameObject> enemyPoolObject2 = new List<GameObject>();

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

    public int enemyCount;

    public Text enemyCountText;

    public int enemyIndex;


    private void Start()
    {
        CreateOriginPool();

        enemyCount = amountToPool;

        enemyIndex = 0;
    }

    private void Update()
    {

        StartCoroutine(SpawnOrigin());

        //if (countDown <= 0.0f)
        //{
        //    StartCoroutine(SpawnOrigin());
        //    countDown = spawnBreakTime;
        //}

        //countDown -= Time.deltaTime;

        enemyCountText.text = enemyCount.ToString();

        if (enemyCount <= 0)
        {
            GameManager.Instance.Pause();
        }

    }

    public void SetEnemyIndex(int Index)
    {
        enemyIndex = Index;
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

            GameObject obj2 = Instantiate(enemyList[1]);
            obj.SetActive(false);
            enemyPoolObject2.Add(obj2);
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

    public GameObject GetOriginPoolObject2()
    {

        for (int i = 0; i < enemyPoolObject2.Count; i++)
        {
            if (!enemyPoolObject2[i].activeInHierarchy)
            {
                return enemyPoolObject2[i];
            }
        }

        return null;

    }


    private void SpawnOriginMode()
    {

        GameObject objects = GetOriginPoolObject();

        GameObject objects2 = GetOriginPoolObject2();

        //int i = Random.Range(0, enemySpawnLocations.Count);

        if (objects != null)
        {
            objects.transform.position = enemySpawnLocations[0].position;
            objects.SetActive(true);
        }

        if (objects2 != null)
        {
            objects2.transform.position = enemySpawnLocations[1].position;
            objects2.SetActive(true);
        }

    }

}
