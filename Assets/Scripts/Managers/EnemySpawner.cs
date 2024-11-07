using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject enemy1Prefab;  // ù ��° ��
    public GameObject enemy2Prefab;  // �� ��° ��
    public GameObject enemy3Prefab;  // �� ��° ��

    [Header("Spawn Settings")]
    public float spawnY = -10f;      // ���� Y ��ġ

    [Header("Enemy 1 Settings")]
    public float enemy1FirstDelay = 0f;   // ���� ���۰� ���ÿ�
    public float enemy1Interval = 7f;     // 7�ʸ���

    [Header("Enemy 2 Settings")]
    public float enemy2FirstDelay = 60f;  // 1�� �� ����
    public float enemy2Interval = 10f;    // 10�ʸ���

    [Header("Enemy 3 Settings")]
    public float enemy3FirstDelay = 120f; // 2�� �� ����
    public float enemy3Interval = 30f;    // 30�ʸ���

    private bool isSpawning = false;

    void Start()
    {
        // ������ ���� �ڵ����� �������� ����
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;

            // ù ��° �� ���� ����
            InvokeRepeating("SpawnEnemy1", enemy1FirstDelay, enemy1Interval);

            // �� ��° �� ���� ����
            InvokeRepeating("SpawnEnemy2", enemy2FirstDelay, enemy2Interval);

            // �� ��° �� ���� ����
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