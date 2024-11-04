using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerdogSpawner : MonoBehaviour
{
    public GameObject[] playerdogPrefabs = new GameObject[5];  // 5���� �������� ���� �迭
    public Button[] spawnButtons = new Button[5];               // 5���� UI ��ư �迭

    private void Start()
    {
        // 5���� ��ư ������ ��ȯ �Լ� ��� (��ư���� ������ �ε����� ����)
        for (int i = 0; i < spawnButtons.Length; i++)
        {
            int index = i;  // �ε����� ���� ������ �����Ͽ� ����
            spawnButtons[i].onClick.AddListener(() => SpawnPrefab(index));
        }
    }

    // �ε����� �ش��ϴ� �������� ��ȯ�ϴ� �Լ�
    private void SpawnPrefab(int index)
    {
        // �������� �迭�� ������ ��쿡�� ��ȯ
        if (index >= 0 && index < playerdogPrefabs.Length && playerdogPrefabs[index] != null)
        {
            // Y ���� �����ϰ� ���� (0���� -20 ����)
            float randomY = Random.Range(-20f, 0f);
            Vector3 spawnPosition = new Vector3(50f, randomY, 0f); // X�� 50, Y�� ���� ��

            Instantiate(playerdogPrefabs[index], spawnPosition, Quaternion.identity);
        }
    }
}
