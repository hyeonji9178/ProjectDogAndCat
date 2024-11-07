using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UltimateSkill : MonoBehaviour
{
    [Header("UI Settings")]
    public Button ultimateButton;        // 필살기 버튼
    public float cooldownTime = 60f;     // 쿨타임 (1분)
    public float initialDelay = 60f;     // 게임 시작 후 첫 사용 가능 시간

    [Header("Skill Settings")]
    public int ultimateDamage = 100;     // 필살기 데미지
    public LayerMask enemyLayer;         // 적 레이어 마스크

    private bool isReady = false;        // 사용 가능 여부
    private Image buttonImage;           // 버튼 이미지
    private Color originalColor;         // 원래 버튼 색상

    void Start()
    {
        // 버튼 이미지 초기화
        buttonImage = ultimateButton.GetComponent<Image>();
        originalColor = buttonImage.color;

        // 버튼에 클릭 이벤트 추가
        ultimateButton.onClick.AddListener(UseUltimate);

        // 버튼 초기 비활성화
        SetButtonState(false);

        // 초기 쿨타임 시작
        StartCoroutine(InitialCooldown());

        Debug.Log("필살기 초기화 완료");
    }

    IEnumerator InitialCooldown()
    {
        Debug.Log("초기 쿨타임 시작");
        yield return new WaitForSeconds(initialDelay);
        Debug.Log("초기 쿨타임 종료, 버튼 활성화");
        SetButtonState(true);
    }

    public void UseUltimate()
    {
        if (!isReady) return;

        Debug.Log("필살기 사용!");

        // 필살기 효과 실행
        ApplyUltimateDamage();

        // 버튼 비활성화 및 쿨타임 시작
        SetButtonState(false);
        StartCoroutine(CooldownRoutine());
    }

    void ApplyUltimateDamage()
    {
        // 맵의 오른쪽 절반에 있는 모든 적 찾기
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            new Vector2(25, 0),          // 중심점 (0~50의 중간)
            new Vector2(50, 100),        // 범위 크기 (가로 50, 세로 100)
            0,                           // 회전 각도
            enemyLayer                   // EnemyUnit 레이어만
        );

        Debug.Log($"감지된 적 수: {hits.Length}");

        foreach (Collider2D hit in hits)
        {
            Unit unit = hit.GetComponent<Unit>();
            if (unit != null && unit.isEnemy)
            {
                Debug.Log($"{hit.name}에게 {ultimateDamage} 데미지!");
                unit.TakeDamage(ultimateDamage);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            new Vector3(25, 0, 0),           // 중심점
            new Vector3(50, 100, 0)          // 크기
        );
    }
}