using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UltimateSkill : MonoBehaviour
{
    [Header("UI Settings")]
    public Button ultimateButton;        // �ʻ�� ��ư
    public float cooldownTime = 60f;     // ��Ÿ�� (1��)
    public float initialDelay = 60f;     // ���� ���� �� ù ��� ���� �ð�

    [Header("Skill Settings")]
    public int ultimateDamage = 100;     // �ʻ�� ������
    public LayerMask enemyLayer;         // �� ���̾� ����ũ

    private bool isReady = false;        // ��� ���� ����
    private Image buttonImage;           // ��ư �̹���
    private Color originalColor;         // ���� ��ư ����

    void Start()
    {
        // ��ư �̹��� �ʱ�ȭ
        buttonImage = ultimateButton.GetComponent<Image>();
        originalColor = buttonImage.color;

        // ��ư�� Ŭ�� �̺�Ʈ �߰�
        ultimateButton.onClick.AddListener(UseUltimate);

        // ��ư �ʱ� ��Ȱ��ȭ
        SetButtonState(false);

        // �ʱ� ��Ÿ�� ����
        StartCoroutine(InitialCooldown());

        Debug.Log("�ʻ�� �ʱ�ȭ �Ϸ�");
    }

    IEnumerator InitialCooldown()
    {
        Debug.Log("�ʱ� ��Ÿ�� ����");
        yield return new WaitForSeconds(initialDelay);
        Debug.Log("�ʱ� ��Ÿ�� ����, ��ư Ȱ��ȭ");
        SetButtonState(true);
    }

    public void UseUltimate()
    {
        if (!isReady) return;

        Debug.Log("�ʻ�� ���!");

        // �ʻ�� ȿ�� ����
        ApplyUltimateDamage();

        // ��ư ��Ȱ��ȭ �� ��Ÿ�� ����
        SetButtonState(false);
        StartCoroutine(CooldownRoutine());
    }

    void ApplyUltimateDamage()
    {
        // ���� ������ ���ݿ� �ִ� ��� �� ã��
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            new Vector2(25, 0),          // �߽��� (0~50�� �߰�)
            new Vector2(50, 100),        // ���� ũ�� (���� 50, ���� 100)
            0,                           // ȸ�� ����
            enemyLayer                   // EnemyUnit ���̾
        );

        Debug.Log($"������ �� ��: {hits.Length}");

        foreach (Collider2D hit in hits)
        {
            Unit unit = hit.GetComponent<Unit>();
            if (unit != null && unit.isEnemy)
            {
                Debug.Log($"{hit.name}���� {ultimateDamage} ������!");
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
            new Vector3(25, 0, 0),           // �߽���
            new Vector3(50, 100, 0)          // ũ��
        );
    }
}