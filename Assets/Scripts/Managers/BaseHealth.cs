using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BaseHealth : MonoBehaviour
{
    public int maxHealth = 1000;
    public int currentHealth;
    public TextMeshProUGUI healthText;  // Slider 대신 TextMeshProUGUI 사용
    public GameObject gameOverUI;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }

    void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}