using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    public int maxHealth = 1000; // 최대 체력
    public int currentHealth;    // 현재 체력
    public TextMeshProUGUI healthTextUI;    // UI Text 컴포넌트 연결 (1000/1000 형식으로 표시)
    
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // 체력 감소 메서드
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI();

        // 체력이 0이 되었을 때의 처리 (예: 기지 파괴, 게임 오버 처리)
        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " 기지 파괴됨!");
            // 추가로 기지가 파괴된 이후 처리 추가 가능
        }
    }

    // 체력 UI 업데이트 (1000/1000 형식)
    private void UpdateHealthUI()
    {
        if (healthTextUI != null)
        {
            healthTextUI.text = currentHealth + " / " + maxHealth;
        }
    }
}
