using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Button[] spawnButtons;
    public SpawnCooldown[] spawnCooldowns;

    public void SpawnPrefab(int index)
    {
        if (index >= 0 && index < prefabs.Length)
        {
            float randomY = Random.Range(-20f, 0f);
            Vector3 spawnPosition = new Vector3(50f, randomY, 0f);

            Instantiate(prefabs[index], spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("ÇÁ¸®ÆÕ ÀÎµ¦½º°¡ ¹üÀ§¸¦ ¹ş¾î³²~");
        }
    }
}