using UnityEngine;
using UnityEngine.UI;

public class SpawnCooldown : MonoBehaviour
{
    public float cooldownTime = 3f;
    public Image cooldownImage;
    public GameObject cooldownUI;  // ��Ÿ�� UI ��ü�� ���� ���� �߰�

    private float cooldownTimer;
    private bool isOnCooldown;

    void Start()
    {
        // ������ �� UI �����
        if (cooldownUI != null)
        {
            cooldownUI.SetActive(false);
        }
    }

    void Update()
    {
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownImage.fillAmount = cooldownTimer / cooldownTime;

            if (cooldownTimer <= 0)
            {
                isOnCooldown = false;
                // ��Ÿ�� ������ UI �����
                if (cooldownUI != null)
                {
                    cooldownUI.SetActive(false);
                }
            }
        }
    }

    public void StartCooldown()
    {
        cooldownTimer = cooldownTime;
        isOnCooldown = true;
        // ��Ÿ�� ������ �� UI ���̱�
        if (cooldownUI != null)
        {
            cooldownUI.SetActive(true);
        }
    }

    public bool IsOnCooldown()
    {
        return isOnCooldown;
    }
}