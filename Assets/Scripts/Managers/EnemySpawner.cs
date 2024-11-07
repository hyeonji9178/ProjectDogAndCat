using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

    [Header("Spawn Settings")]
    public float spawnY = -10f;
    public float spawnX = -50f;  // ���ʿ��� �����ϵ��� X ��ġ �߰�

    [Header("Enemy 1 Settings")]
    public float enemy1FirstDelay = 0f;
    public float enemy1Interval = 7f;

    [Header("Enemy 2 Settings")]
    public float enemy2FirstDelay = 60f;
    public float enemy2Interval = 10f;

    [Header("Enemy 3 Settings")]
    public float enemy3FirstDelay = 120f;
    public float enemy3Interval = 30f;

    private bool isSpawning = false;

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            Debug.Log("�� ���� ����!"); // ����� �α� �߰�
            isSpawning = true;

            // �� �� Ÿ�Ժ� ���� ����
            if (enemy1Prefab != null)
            {
                InvokeRepeating("SpawnEnemy1", enemy1FirstDelay, enemy1Interval);
            }

            if (enemy2Prefab != null)
            {
                InvokeRepeating("SpawnEnemy2", enemy2FirstDelay, enemy2Interval);
            }

            if (enemy3Prefab != null)
            {
                InvokeRepeating("SpawnEnemy3", enemy3FirstDelay, enemy3Interval);
            }
        }
    }

    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            CancelInvoke();
        }
    }

    void SpawnEnemy1()
    {
        float randomY = Random.Range(-20f, 0f);  // Y ��ġ ����ȭ
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0);
        if (enemy1Prefab != null)
        {
            Instantiate(enemy1Prefab, spawnPosition, Quaternion.identity);
            Debug.Log("Enemy1 ������!"); // ����� �α� �߰�
        }
    }

    void SpawnEnemy2()
    {
        float randomY = Random.Range(-20f, 0f);
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0);
        if (enemy2Prefab != null)
        {
            Instantiate(enemy2Prefab, spawnPosition, Quaternion.identity);
            Debug.Log("Enemy2 ������!");
        }
    }

    void SpawnEnemy3()
    {
        float randomY = Random.Range(-20f, 0f);
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0);
        if (enemy3Prefab != null)
        {
            Instantiate(enemy3Prefab, spawnPosition, Quaternion.identity);
            Debug.Log("Enemy3 ������!");
        }
    }
}