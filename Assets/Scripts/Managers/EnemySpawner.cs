using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1; // 첫 번째 적 프리팹
    public GameObject enemyPrefab2; // 두 번째 적 프리팹
    public GameObject enemyPrefab3; // 세 번째 적 프리팹

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyCoroutine(enemyPrefab1, 7f)); // 첫 번째 적을 즉시 스폰하고 7초마다 계속 스폰
        StartCoroutine(SpawnSecondEnemy()); // 두 번째 적 스폰 코루틴 시작
        StartCoroutine(SpawnThirdEnemy()); // 세 번째 적 스폰 코루틴 시작
    }

    private IEnumerator SpawnEnemyCoroutine(GameObject enemyPrefab, float interval)
    {
        // 첫 번째 적 즉시 스폰
        SpawnEnemy(enemyPrefab);

        // 이후 7초 간격으로 계속 스폰
        while (true)
        {
            yield return new WaitForSeconds(interval);
            SpawnEnemy(enemyPrefab);
        }
    }

    private IEnumerator SpawnSecondEnemy()
    {
        yield return new WaitForSeconds(60f); // 1분 대기
        while (true)
        {
            SpawnEnemy(enemyPrefab2);
            yield return new WaitForSeconds(10f); // 10초 간격으로 두 번째 적 스폰
        }
    }

    private IEnumerator SpawnThirdEnemy()
    {
        yield return new WaitForSeconds(120f); // 2분 대기
        while (true)
        {
            SpawnEnemy(enemyPrefab3);
            yield return new WaitForSeconds(30f); // 30초 간격으로 세 번째 적 스폰
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 spawnPos = new Vector3(-50, Random.Range(0, -20), 0); // X = -50, Y = 0에서 -20까지

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().Initialize(spawnPos);
    }
}
