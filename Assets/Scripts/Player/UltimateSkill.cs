using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UltimateSkill : MonoBehaviour
{
    public Button ultimateButton;        // 필살기 버튼
    public float cooldownTime = 60f;     // 쿨타임 (1분)
    public float initialDelay = 60f;     // 게임 시작 후 첫 사용 가능 시간
    public int ultimateDamage = 100000;  // 필살기 데미지

    private bool isReady = false;        // 사용 가능 여부
    private Image buttonImage;           // 버튼 이미지
    private Color originalColor;         // 원래 버튼 색상

    void Start()
    {
        buttonImage = ultimateButton.GetComponent<Image>();
        originalColor = buttonImage.color;

        // 버튼 초기 비활성화
        SetButtonState(false);

        // 1분 후 첫 사용 가능
        StartCoroutine(InitialCooldown());
    }

    IEnumerator InitialCooldown()
    {
        yield return new WaitForSeconds(initialDelay);
        SetButtonState(true);
    }

    public void UseUltimate()
    {
        if (!isReady) return;

        // 필살기 효과 실행
        ApplyUltimateDamage();

        // 버튼 비활성화 및 쿨타임 시작
        SetButtonState(false);
        StartCoroutine(CooldownRoutine());
    }

    void ApplyUltimateDamage()
    {
        // 맵 중앙(0,0)부터 아군 기지(x:50)까지의 모든 적 찾기
        Collider2D[] hits = Physics2D.OverlapAreaAll(
            new Vector2(0, -50),     // 왼쪽 아래 지점
            new Vector2(50, 50)      // 오른쪽 위 지점
        );

        foreach (Collider2D hit in hits)
        {
            // Enemy 컴포넌트를 가진 오브젝트만 데미지
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(ultimateDamage);
            }
        }
    }

    void SetButtonState(bool state)
    {
        isReady = state;
        buttonImage.color = state ? originalColor : Color.gray;
        ultimateButton.interactable = state;
    }

    IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        SetButtonState(true);
    }

    // 필살기 사용 가능 여부 확인 (레벨업 시스템에서 사용)
    public bool IsUltimateReady()
    {
        return isReady;
    }
}