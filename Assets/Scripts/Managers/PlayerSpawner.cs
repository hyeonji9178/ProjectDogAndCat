using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerdogSpawner : MonoBehaviour
{
    public GameObject[] playerdogPrefabs = new GameObject[5];  // 5개의 프리팹을 담을 배열
    public Button[] spawnButtons = new Button[5];               // 5개의 UI 버튼 배열

    private void Start()
    {
        // 5개의 버튼 각각에 소환 함수 등록 (버튼마다 고유의 인덱스를 전달)
        for (int i = 0; i < spawnButtons.Length; i++)
        {
            int index = i;  // 인덱스를 로컬 변수로 고정하여 전달
            spawnButtons[i].onClick.AddListener(() => SpawnPrefab(index));
        }
    }

    // 인덱스에 해당하는 프리팹을 소환하는 함수
    private void SpawnPrefab(int index)
    {
        // 프리팹이 배열에 존재할 경우에만 소환
        if (index >= 0 && index < playerdogPrefabs.Length && playerdogPrefabs[index] != null)
        {
            // Y 값을 랜덤하게 설정 (0부터 -20 사이)
            float randomY = Random.Range(-20f, 0f);
            Vector3 spawnPosition = new Vector3(50f, randomY, 0f); // X는 50, Y는 랜덤 값

            Instantiate(playerdogPrefabs[index], spawnPosition, Quaternion.identity);
        }
    }
}
