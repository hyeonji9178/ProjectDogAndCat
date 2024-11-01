using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour //���ӸŴ���
{
    public GameObject introScreen;  // ��Ʈ�� ȭ��
    public Button startButton;       // ���� ��ư
    public Button levelUpButton;     // ������ ��ư
    public TextMeshProUGUI moneyText;           // �� ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI levelText;           // ���� ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI levelUpButtonPrice;   // ������ ���� �ؽ�Ʈ
    public int Money { get; private set; } = 0; // �ʱ� ���� public���� ����
    public int money = 0;           // �ʱ� ��
    public int level = 1;           // �ʱ� ����
    public int moneyPerSecond = 6;  // �ʴ� ������̴� ��
    public int maxMoney = 100;      // �ִ� ��
    public int levelUpCost = 40;    // �ʱ� ������ ���

    void Start()
    {
        // ��ư Ŭ�� �� �Լ� ȣ��
        startButton.onClick.AddListener(StartGame);
        levelUpButton.onClick.AddListener(LevelUp);

        // �ʱ� ���� ���� ǥ��
        UpdateMoneyText();
        UpdateLevelText();
        UpdateLevelUpButtonText(); // ������ ��ư �ؽ�Ʈ �ʱ�ȭ
    }

    void StartGame()
    {
        // ��Ʈ�� ȭ�� ��Ȱ��ȭ
        introScreen.SetActive(false);

        // �� ���� �ڷ�ƾ ����
        StartCoroutine(IncreaseMoney());
    }

    IEnumerator IncreaseMoney()
    {
        while (true)
        {
            // 1�� ���
            yield return new WaitForSeconds(1f);

            // 1�� ���� moneyPerSecond�� ���� 1���� ����
            for (int i = 0; i < moneyPerSecond; i++)
            {
                // �� ����
                if (money < maxMoney)
                {
                    money += 1; // 1���� ����
                    if (money > maxMoney) money = maxMoney;  // �ִ� �� ����
                }

                // �� ǥ�� ������Ʈ
                UpdateMoneyText();

                // 1�ʸ� moneyPerSecond�� ������ ���
                yield return new WaitForSeconds(1f / moneyPerSecond); // moneyPerSecond�� ���� ���
            }
        }
    }

    void LevelUp()
    {
        // ������ ���� ���� Ȯ�� �� ������ 7 �̸����� Ȯ��
        if (money >= levelUpCost && level < 7)
        {
            // �� ����
            money -= levelUpCost;

            // ���� ����
            level++;

            // �ʴ� ������̴� �� ����
            moneyPerSecond += 4;

            // �ִ� �� ����
            maxMoney += 50;

            // ������ ��� ����
            levelUpCost += 40; // ������ ����� 40�� ����

            // �� �� ���� ǥ�� ������Ʈ
            UpdateMoneyText();
            UpdateLevelText();
            UpdateLevelUpButtonText(); // ������ ��ư �ؽ�Ʈ ������Ʈ
        }
    }

    void UpdateMoneyText()
    {
        moneyText.text = money.ToString() + "/" + maxMoney.ToString() + " ��"; // ���� ����
    }

    void UpdateLevelText()
    {
        levelText.text = "LV. " + level.ToString();
    }

    void UpdateLevelUpButtonText()
    {
        // ������ 7�� �� MAX LEVEL ǥ��
        if (level >= 7)
        {
            levelUpButtonPrice.text = "MAX LEVEL"; // ��ư �ؽ�Ʈ�� MAX LEVEL�� ����
        }
        else
        {
            levelUpButtonPrice.text = levelUpCost.ToString() + " ��"; // ��ư �ؽ�Ʈ ������Ʈ
        }
    }
}
