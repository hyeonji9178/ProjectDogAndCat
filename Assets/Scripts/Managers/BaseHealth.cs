using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    public int maxHealth = 1000; // �ִ� ü��
    public int currentHealth;    // ���� ü��
    public TextMeshProUGUI healthTextUI;    // UI Text ������Ʈ ���� (1000/1000 �������� ǥ��)
    
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // ü�� ���� �޼���
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI();

        // ü���� 0�� �Ǿ��� ���� ó�� (��: ���� �ı�, ���� ���� ó��)
        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " ���� �ı���!");
            // �߰��� ������ �ı��� ���� ó�� �߰� ����
        }
    }

    // ü�� UI ������Ʈ (1000/1000 ����)
    private void UpdateHealthUI()
    {
        if (healthTextUI != null)
        {
            healthTextUI.text = currentHealth + " / " + maxHealth;
        }
    }
}
