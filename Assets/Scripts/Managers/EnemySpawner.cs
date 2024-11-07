using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float firstSpawnDelay = 7f;  // 첫 스폰 대기 시간
    public float spawnInterval = 5f;     // 스폰 간격
    public float spawnY = -10f;         // 스폰 Y 위치

    private bool isSpawning = false;

    void Start()
    {
        // 시작할 때 자동으로 시작하지 않음
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            // 첫 스폰은 7초 후에, 그 다음부터는 5초 간격으로
            InvokeRepeating("SpawnEnemy", firstSpawnDelay, spawnInterval);
        }
    }

    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            CancelInvoke("SpawnEnemy");
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}