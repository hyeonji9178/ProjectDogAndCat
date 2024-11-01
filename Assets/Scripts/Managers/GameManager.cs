using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour //게임매니저
{
    public GameObject introScreen;  // 인트로 화면
    public Button startButton;       // 시작 버튼
    public Button levelUpButton;     // 레벨업 버튼
    public TextMeshProUGUI moneyText;           // 돈 표시 텍스트
    public TextMeshProUGUI levelText;           // 레벨 표시 텍스트
    public TextMeshProUGUI levelUpButtonPrice;   // 레벨업 가격 텍스트
    public int Money { get; private set; } = 0; // 초기 돈을 public으로 설정
    public int money = 0;           // 초기 돈
    public int level = 1;           // 초기 레벨
    public int moneyPerSecond = 6;  // 초당 벌어들이는 돈
    public int maxMoney = 100;      // 최대 돈
    public int levelUpCost = 40;    // 초기 레벨업 비용

    void Start()
    {
        // 버튼 클릭 시 함수 호출
        startButton.onClick.AddListener(StartGame);
        levelUpButton.onClick.AddListener(LevelUp);

        // 초기 돈과 레벨 표시
        UpdateMoneyText();
        UpdateLevelText();
        UpdateLevelUpButtonText(); // 레벨업 버튼 텍스트 초기화
    }

    void StartGame()
    {
        // 인트로 화면 비활성화
        introScreen.SetActive(false);

        // 돈 증가 코루틴 시작
        StartCoroutine(IncreaseMoney());
    }

    IEnumerator IncreaseMoney()
    {
        while (true)
        {
            // 1초 대기
            yield return new WaitForSeconds(1f);

            // 1초 동안 moneyPerSecond에 따라 1원씩 증가
            for (int i = 0; i < moneyPerSecond; i++)
            {
                // 돈 증가
                if (money < maxMoney)
                {
                    money += 1; // 1원씩 증가
                    if (money > maxMoney) money = maxMoney;  // 최대 돈 제한
                }

                // 돈 표시 업데이트
                UpdateMoneyText();

                // 1초를 moneyPerSecond로 나누어 대기
                yield return new WaitForSeconds(1f / moneyPerSecond); // moneyPerSecond에 따라 대기
            }
        }
    }

    void LevelUp()
    {
        // 레벨업 가능 여부 확인 및 레벨이 7 미만인지 확인
        if (money >= levelUpCost && level < 7)
        {
            // 돈 감소
            money -= levelUpCost;

            // 레벨 증가
            level++;

            // 초당 벌어들이는 돈 증가
            moneyPerSecond += 4;

            // 최대 돈 증가
            maxMoney += 50;

            // 레벨업 비용 증가
            levelUpCost += 40; // 레벨업 비용을 40씩 증가

            // 돈 및 레벨 표시 업데이트
            UpdateMoneyText();
            UpdateLevelText();
            UpdateLevelUpButtonText(); // 레벨업 버튼 텍스트 업데이트
        }
    }

    void UpdateMoneyText()
    {
        moneyText.text = money.ToString() + "/" + maxMoney.ToString() + " 원"; // 형식 변경
    }

    void UpdateLevelText()
    {
        levelText.text = "LV. " + level.ToString();
    }

    void UpdateLevelUpButtonText()
    {
        // 레벨이 7일 때 MAX LEVEL 표시
        if (level >= 7)
        {
            levelUpButtonPrice.text = "MAX LEVEL"; // 버튼 텍스트를 MAX LEVEL로 설정
        }
        else
        {
            levelUpButtonPrice.text = levelUpCost.ToString() + " 원"; // 버튼 텍스트 업데이트
        }
    }
}
