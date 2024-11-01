using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1; // ù ��° �� ������
    public GameObject enemyPrefab2; // �� ��° �� ������
    public GameObject enemyPrefab3; // �� ��° �� ������

    public Vector2 spawnXRange = new Vector2(-50, -50); // x�� ���� ����
    public Vector2 spawnYRange = new Vector2(0, -20); // y�� ���� ����

    public void StartSpawning()
    {
        // ù ��° ��: ��� ����, ���� 7�� �������� ����
        SpawnEnemy(enemyPrefab1); // �������ڸ��� ù ����
        StartCoroutine(SpawnEnemyCoroutine(enemyPrefab1, 7f));

        // �� ��° ��: 1�� �� ù ����, ���� 10�� �������� ����
        StartCoroutine(DelayedSpawn(enemyPrefab2, 60f, 10f));

        // �� ��° ��: 2�� �� ù ����, ���� 30�� �������� ����
        StartCoroutine(DelayedSpawn(enemyPrefab3, 120f, 30f));
    }

    private IEnumerator SpawnEnemyCoroutine(GameObject enemyPrefab, float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            SpawnEnemy(enemyPrefab);
        }
    }

    private IEnumerator DelayedSpawn(GameObject enemyPrefab, float initialDelay, float interval)
    {
        yield return new WaitForSeconds(initialDelay); // ù ���� ��� �ð�
        SpawnEnemy(enemyPrefab); // ù ����

        // ���� ���� �������� �ݺ� ����
        while (true)
        {
            yield return new WaitForSeconds(interval);
            SpawnEnemy(enemyPrefab);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(spawnXRange.x, spawnXRange.y),
            Random.Range(spawnYRange.x, spawnYRange.y),
            0);

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().Initialize(spawnPos); // ���� �ʱ�ȭ ȣ��
    }
}
