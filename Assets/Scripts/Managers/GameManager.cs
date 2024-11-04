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

    public int Money { get; private set; } = 0;
    public int money = 0;
    public int level = 1;
    public int moneyPerSecond = 6;  // �ʱ� �ʴ� ������̴� ��
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
            moneyPerSecond += 4;  // ������ �� 4�� ����
            maxMoney += 50;
            levelUpCost += 40;

            UpdateMoneyText();
            UpdateLevelText();
            UpdateLevelUpButtonText();
        }
    }

    void UpdateMoneyText()
    {
        moneyText.text = $"{money}/{maxMoney} ��";
    }

    void UpdateLevelText()
    {
        levelText.text = $"LV. {level}";
    }

    void UpdateLevelUpButtonText()
    {
        levelUpButtonPrice.text = level >= 7 ? "MAX LEVEL" : $"{levelUpCost} ��";
    }
}
