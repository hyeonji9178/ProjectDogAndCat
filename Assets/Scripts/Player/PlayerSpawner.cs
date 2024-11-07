using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Button[] spawnButtons; // 5개의 버튼을 연결할 배열

    private void Start()
    {
        // 각 버튼에 클릭 이벤트 연결
        for (int i = 0; i < spawnButtons.Length; i++)
        {
            int index = i; // 클로저 문제를 피하기 위해 지역 변수 사용
            spawnButtons[i].onClick.AddListener(() => SpawnPrefab(index));
        }
    }

    public void SpawnPrefab(int index)
    {
        // x는 50, y는 0에서 -20 사이 랜덤값
        float randomY = Random.Range(-20f, 0f);
        Vector3 spawnPosition = new Vector3(50f, randomY, 0f);

        if (index >= 0 && index < prefabs.Length)
        {
            Instantiate(prefabs[index], spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("프리팹 인덱스가 범위를 벗어났습니다.");
        }
    }
}