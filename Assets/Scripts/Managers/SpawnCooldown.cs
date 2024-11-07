using UnityEngine;
using UnityEngine.UI;

public class SpawnCooldown : MonoBehaviour
{
    public Image cooldownImage;      // ��Ÿ���� ������ �̹���
    public float cooldownTime = 3f;  // ��Ÿ�� �ð� (��)

    private float currentCooldown;
    private bool isCooldown = false;

    void Start()
    {
        // ó������ ��Ÿ�� ����
        cooldownImage.fillAmount = 0;
    }

    void Update()
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;
            cooldownImage.fillAmount = currentCooldown / cooldownTime;

            if (currentCooldown <= 0)
            {
                isCooldown = false;
                cooldownImage.fillAmount = 0;
            }
        }
    }

    public void StartCooldown()
    {
        currentCooldown = cooldownTime;
        isCooldown = true;
        cooldownImage.fillAmount = 1;
    }

    public bool IsOnCooldown()
    {
        return isCooldown;
    }
}