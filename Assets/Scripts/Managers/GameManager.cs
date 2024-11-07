using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject introScreen;
    public GameObject gameScreen;
    public Button startButton;
    public Button levelUpButton;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI levelUpButtonPrice;
    public EnemySpawner enemySpawner;

    public int money = 0;  // 시작 금액 0으로 수정
    public int level = 1;
    public int moneyPerSecond = 6;  // 초기 초당 벌어들이는 돈
    public int maxMoney = 100;
    public int levelUpCost = 40;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        levelUpButton.onClick.AddListener(LevelUp);

        UpdateMoneyText();
        UpdateLevelText();
        UpdateLevelUpButtonText();
    }

    public void StartGame()
    {
        introScreen.SetActive(false);
        gameScreen.SetActive(true);

        if (enemySpawner != null)
        {
            enemySpawner.StartSpawning();
        }
        else
        {
            Debug.LogError("EnemySpawner is not assigned in the GameManager.");
        }

        StartCoroutine(IncreaseMoney());
    }

    IEnumerator IncreaseMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (money < maxMoney)
            {
                money += moneyPerSecond;
                if (money > maxMoney) money = maxMoney;
            }
            UpdateMoneyText();
        }
    }

    void LevelUp()
    {
        if (money >= levelUpCost && level < 7)
        {
            money -= levelUpCost;
            level++;
            moneyPerSecond += 4;  // 레벨업 시 4원 증가
            maxMoney += 50;
            levelUpCost += 40;

            UpdateMoneyText();
            UpdateLevelText();
            UpdateLevelUpButtonText();
        }
    }

    void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = $"{money}/{maxMoney} 원";
        }
    }

    void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = $"LV. {level}";
        }
    }

    void UpdateLevelUpButtonText()
    {
        if (levelUpButtonPrice != null)
        {
            levelUpButtonPrice.text = level >= 7 ? "MAX LEVEL" : $"{levelUpCost} 원";
        }
    }
}