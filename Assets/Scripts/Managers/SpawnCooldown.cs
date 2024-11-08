using UnityEngine;
using UnityEngine.UI;

public class SpawnCooldown : MonoBehaviour
{
    [Header("Cooldown Settings")]
    public float cooldownTime = 3f;
    public bool canSpawn = true;

    [Header("UI References")]
    public Image cooldownImage;
    public GameObject cooldownUI;

    private float cooldownTimer;
    public bool isOnCooldown;



    void Start()
    {
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0;
        }
    }

    void Update()
    {
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownImage != null)
            {

                cooldownImage.fillAmount = (cooldownTimer / cooldownTime);
                Debug.Log($"Fill Amount: {cooldownImage.fillAmount}"); // ����׿�
            }

            if (cooldownTimer <= 0)
            {
                isOnCooldown = false;
                if (cooldownImage != null)
                {
                    cooldownImage.fillAmount = 0;
                }
            }
        }
    }

    public void StartCooldown()
    {
        Debug.Log("starcooldown ����");
        if (canSpawn) return;
        Debug.Log("starcooldown ����");
        cooldownTimer = cooldownTime;


        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 1;
        }
    }

    public bool IsOnCooldown()
    {
        return isOnCooldown;
    }
}