using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Button[] spawnButtons; // 5���� ��ư�� ������ �迭

    private void Start()
    {
        // �� ��ư�� Ŭ�� �̺�Ʈ ����
        for (int i = 0; i < spawnButtons.Length; i++)
        {
            int index = i; // Ŭ���� ������ ���ϱ� ���� ���� ���� ���
            spawnButtons[i].onClick.AddListener(() => SpawnPrefab(index));
        }
    }

    public void SpawnPrefab(int index)
    {
        // x�� 50, y�� 0���� -20 ���� ������
        float randomY = Random.Range(-20f, 0f);
        Vector3 spawnPosition = new Vector3(50f, randomY, 0f);

        if (index >= 0 && index < prefabs.Length)
        {
            Instantiate(prefabs[index], spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("������ �ε����� ������ ������ϴ�.");
        }
    }
}