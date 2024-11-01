using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1; // 첫 번째 적 프리팹
    public GameObject enemyPrefab2; // 두 번째 적 프리팹
    public GameObject enemyPrefab3; // 세 번째 적 프리팹

    public Vector2 spawnXRange = new Vector2(-50, -50); // x축 고정 범위
    public Vector2 spawnYRange = new Vector2(0, -20); // y축 랜덤 범위

    public void StartSpawning()
    {
        // 첫 번째 적: 즉시 스폰, 이후 7초 간격으로 스폰
        SpawnEnemy(enemyPrefab1); // 시작하자마자 첫 스폰
        StartCoroutine(SpawnEnemyCoroutine(enemyPrefab1, 7f));

        // 두 번째 적: 1분 후 첫 생성, 이후 10초 간격으로 스폰
        StartCoroutine(DelayedSpawn(enemyPrefab2, 60f, 10f));

        // 세 번째 적: 2분 후 첫 생성, 이후 30초 간격으로 스폰
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
        yield return new WaitForSeconds(initialDelay); // 첫 생성 대기 시간
        SpawnEnemy(enemyPrefab); // 첫 스폰

        // 이후 일정 간격으로 반복 생성
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
        enemy.GetComponent<Enemy>().Initialize(spawnPos); // 적의 초기화 호출
    }
}
