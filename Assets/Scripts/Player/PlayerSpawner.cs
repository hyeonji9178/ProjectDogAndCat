using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Button[] spawnButtons;
    public SpawnCooldown[] spawnCooldowns;

    private void Start()
    {
        for (int i = 0; i < spawnButtons.Length; i++)
        {
            int index = i;
            spawnButtons[i].onClick.AddListener(() => TrySpawnPrefab(index));
        }
    }

    // CharacterShopSystem에서 호출하는 메서드
    public void SpawnPrefab(int index)
    {
        TrySpawnPrefab(index);
    }

    public void TrySpawnPrefab(int index)
    {
        if (spawnCooldowns[index].IsOnCooldown())
        {
            return;
        }

        float randomY = Random.Range(-20f, 0f);
        Vector3 spawnPosition = new Vector3(50f, randomY, 0f);

        if (index >= 0 && index < prefabs.Length)
        {
            Instantiate(prefabs[index], spawnPosition, Quaternion.identity);
            spawnCooldowns[index].StartCooldown();
        }
        else
        {
            Debug.LogWarning("프리팹 인덱스가 범위를 벗어났습니다.");
        }
    }
}