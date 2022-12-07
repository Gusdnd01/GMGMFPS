using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    private List<int> uesdSpawnPoint = new List<int>();

    public GameObject enemyPrefab;
    public int spawnNumber = 0;

    private Transform playerTrm;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }

        playerTrm = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        Spawn();
    }

    private void Update()
    {
        if(CheckSpawn())
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        uesdSpawnPoint.Clear();
        enemies.Clear();

        for (int i = 0; i < spawnNumber; i++)
        {
            Transform spawnPoint = spawnPoints[SetSpawnPointValue()];

            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, SetRotation(spawnPoint)); //나중에 오브젝트 풀 사용 개객꺄
            enemies.Add(enemy);
            Debug.Log("spawn");
        }      
    }

    private bool CheckSpawn()
    {
        if(enemies.Count > 0)
        {
            return false;
        }

        return true;
    }

    private int SetSpawnPointValue()
    {
        int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Count);

        for(int i = 0; i < uesdSpawnPoint.Count; i++)
        {
            if(uesdSpawnPoint[i] == spawnPointIndex)
            {
                return SetSpawnPointValue();
            }
        }

        uesdSpawnPoint.Add(spawnPointIndex);

        return spawnPointIndex;
    }

    private Quaternion SetRotation(Transform _spawnPoint)
    {
        Vector3 dir = (playerTrm.position - _spawnPoint.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        return Quaternion.Euler(0, angle, 0);
    }

    public void Remove(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}
