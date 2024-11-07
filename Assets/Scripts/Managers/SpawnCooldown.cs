using UnityEngine;
using UnityEngine.UI;

public class SpawnCooldown : MonoBehaviour
{
    public Image cooldownImage;      // 쿨타임을 보여줄 이미지
    public float cooldownTime = 3f;  // 쿨타임 시간 (초)

    private float currentCooldown;
    private bool isCooldown = false;

    void Start()
    {
        // 처음에는 쿨타임 없음
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