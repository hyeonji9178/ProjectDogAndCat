using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UltimateSkill : MonoBehaviour
{
    public Button ultimateButton;        // �ʻ�� ��ư
    public float cooldownTime = 60f;     // ��Ÿ�� (1��)
    public float initialDelay = 60f;     // ���� ���� �� ù ��� ���� �ð�
    public int ultimateDamage = 100000;  // �ʻ�� ������

    private bool isReady = false;        // ��� ���� ����
    private Image buttonImage;           // ��ư �̹���
    private Color originalColor;         // ���� ��ư ����

    void Start()
    {
        buttonImage = ultimateButton.GetComponent<Image>();
        originalColor = buttonImage.color;

        // ��ư �ʱ� ��Ȱ��ȭ
        SetButtonState(false);

        // 1�� �� ù ��� ����
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

        // �ʻ�� ȿ�� ����
        ApplyUltimateDamage();

        // ��ư ��Ȱ��ȭ �� ��Ÿ�� ����
        SetButtonState(false);
        StartCoroutine(CooldownRoutine());
    }

    void ApplyUltimateDamage()
    {
        // �� �߾�(0,0)���� �Ʊ� ����(x:50)������ ��� �� ã��
        Collider2D[] hits = Physics2D.OverlapAreaAll(
            new Vector2(0, -50),     // ���� �Ʒ� ����
            new Vector2(50, 50)      // ������ �� ����
        );

        foreach (Collider2D hit in hits)
        {
            // Enemy ������Ʈ�� ���� ������Ʈ�� ������
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

    // �ʻ�� ��� ���� ���� Ȯ�� (������ �ý��ۿ��� ���)
    public bool IsUltimateReady()
    {
        return isReady;
    }
}