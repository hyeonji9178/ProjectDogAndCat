using UnityEngine;
using UnityEngine.UI;

public class SpawnCooldown : MonoBehaviour
{
    public float cooldownTime = 3f;
    public Image cooldownImage;
    public GameObject cooldownUI;  // 쿨타임 UI 전체를 담을 변수 추가

    private float cooldownTimer;
    private bool isOnCooldown;

    void Start()
    {
        // 시작할 때 UI 숨기기
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
                // 쿨타임 끝나면 UI 숨기기
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
        // 쿨타임 시작할 때 UI 보이기
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