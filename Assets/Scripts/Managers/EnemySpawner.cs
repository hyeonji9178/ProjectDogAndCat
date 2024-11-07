using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject enemy1Prefab;  // 첫 번째 적
    public GameObject enemy2Prefab;  // 두 번째 적
    public GameObject enemy3Prefab;  // 세 번째 적

    [Header("Spawn Settings")]
    public float spawnY = -10f;      // 스폰 Y 위치

    [Header("Enemy 1 Settings")]
    public float enemy1FirstDelay = 0f;   // 게임 시작과 동시에
    public float enemy1Interval = 7f;     // 7초마다

    [Header("Enemy 2 Settings")]
    public float enemy2FirstDelay = 60f;  // 1분 후 시작
    public float enemy2Interval = 10f;    // 10초마다

    [Header("Enemy 3 Settings")]
    public float enemy3FirstDelay = 120f; // 2분 후 시작
    public float enemy3Interval = 30f;    // 30초마다

    private bool isSpawning = false;

    void Start()
    {
        // 시작할 때는 자동으로 시작하지 않음
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;

            // 첫 번째 적 스폰 시작
            InvokeRepeating("SpawnEnemy1", enemy1FirstDelay, enemy1Interval);

            // 두 번째 적 스폰 시작
            InvokeRepeating("SpawnEnemy2", enemy2FirstDelay, enemy2Interval);

            // 세 번째 적 스폰 시작
            InvokeRepeating("SpawnEnemy3", enemy3FirstDelay, enemy3Interval);
        }
    }

    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            CancelInvoke("SpawnEnemy1");
            CancelInvoke("SpawnEnemy2");
            CancelInvoke("SpawnEnemy3");
        }
    }

    void SpawnEnemy1()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0);
        Instantiate(enemy1Prefab, spawnPosition, Quaternion.identity);
    }

    void SpawnEnemy2()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0);
        Instantiate(enemy2Prefab, spawnPosition, Quaternion.identity);
    }

    void SpawnEnemy3()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0);
        Instantiate(enemy3Prefab, spawnPosition, Quaternion.identity);
    }
}