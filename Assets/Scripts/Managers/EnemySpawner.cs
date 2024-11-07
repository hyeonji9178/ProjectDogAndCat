using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float firstSpawnDelay = 7f;  // ù ���� ��� �ð�
    public float spawnInterval = 5f;     // ���� ����
    public float spawnY = -10f;         // ���� Y ��ġ

    private bool isSpawning = false;

    void Start()
    {
        // ������ �� �ڵ����� �������� ����
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            // ù ������ 7�� �Ŀ�, �� �������ʹ� 5�� ��������
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