using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BlinkLevelUpButton : MonoBehaviour
{
    public Button grayLevelUpButton;    // 회색 레벨업 버튼
    public Button normalLevelUpButton;  // 원래 레벨업 버튼
    private GameManager gameManager;    // GameManager 스크립트 참조

    private float blinkDuration = 0.5f; // 반짝이는 지속 시간
    private float blinkInterval = 0.1f; // 반짝이는 간격
    private bool isBlinking = false;    // 현재 깜빡이는 중인지 여부

    void Start()
    {
        // GameManager를 찾아서 할당
        gameManager = FindObjectOfType<GameManager>();
        grayLevelUpButton.gameObject.SetActive(false); // 초기에는 회색 버튼 숨김
    }

    void Update()
    {
        // 레벨업 버튼의 가격이 돈보다 클 경우
        if (gameManager.money < gameManager.levelUpCost)
        {
            grayLevelUpButton.gameObject.SetActive(true); // 회색 버튼 보이기

            // 돈이 레벨업 가능할 때만 깜빡이기 시작
            if (gameManager.money >= gameManager.levelUpCost && !isBlinking)
            {
                StartCoroutine(BlinkGrayButton());
            }
        }
        else
        {
            // 돈이 충분하면 회색 버튼을 숨김
            grayLevelUpButton.gameObject.SetActive(false);
            if (isBlinking)
            {
                StopAllCoroutines();
                isBlinking = false;
            }
        }
    }

    IEnumerator BlinkGrayButton()
    {
        isBlinking = true;

        for (int i = 0; i < 5; i++) // 5회 깜빡임
        {
            grayLevelUpButton.gameObject.SetActive(true); // 회색 버튼 보이기
            yield return new WaitForSeconds(blinkDuration); // 0.5초 동안 보이기
            grayLevelUpButton.gameObject.SetActive(false); // 회색 버튼 숨기기
            yield return new WaitForSeconds(blinkInterval); // 0.1초 동안 숨기기
        }

        // 깜빡임이 끝난 후 회색 버튼을 계속 숨김
        grayLevelUpButton.gameObject.SetActive(false); // 깜빡임 후 숨김
        isBlinking = false;
    }

    public void OnLevelUpButtonClicked()
    {
        // 레벨업 버튼 클릭 시 호출되는 함수
        grayLevelUpButton.gameObject.SetActive(false); // 회색 버튼 숨기기
        StopAllCoroutines(); // 깜빡임 중지
        isBlinking = false;
    }
}
