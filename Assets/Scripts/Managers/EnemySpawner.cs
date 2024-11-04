using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1; // ù ��° �� ������
    public GameObject enemyPrefab2; // �� ��° �� ������
    public GameObject enemyPrefab3; // �� ��° �� ������

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyCoroutine(enemyPrefab1, 7f)); // ù ��° ���� ��� �����ϰ� 7�ʸ��� ��� ����
        StartCoroutine(SpawnSecondEnemy()); // �� ��° �� ���� �ڷ�ƾ ����
        StartCoroutine(SpawnThirdEnemy()); // �� ��° �� ���� �ڷ�ƾ ����
    }

    private IEnumerator SpawnEnemyCoroutine(GameObject enemyPrefab, float interval)
    {
        // ù ��° �� ��� ����
        SpawnEnemy(enemyPrefab);

        // ���� 7�� �������� ��� ����
        while (true)
        {
            yield return new WaitForSeconds(interval);
            SpawnEnemy(enemyPrefab);
        }
    }

    private IEnumerator SpawnSecondEnemy()
    {
        yield return new WaitForSeconds(60f); // 1�� ���
        while (true)
        {
            SpawnEnemy(enemyPrefab2);
            yield return new WaitForSeconds(10f); // 10�� �������� �� ��° �� ����
        }
    }

    private IEnumerator SpawnThirdEnemy()
    {
        yield return new WaitForSeconds(120f); // 2�� ���
        while (true)
        {
            SpawnEnemy(enemyPrefab3);
            yield return new WaitForSeconds(30f); // 30�� �������� �� ��° �� ����
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 spawnPos = new Vector3(-50, Random.Range(0, -20), 0); // X = -50, Y = 0���� -20����

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().Initialize(spawnPos);
    }
}
