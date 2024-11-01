using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BlinkLevelUpButton : MonoBehaviour
{
    public Button grayLevelUpButton;    // ȸ�� ������ ��ư
    public Button normalLevelUpButton;  // ���� ������ ��ư
    private GameManager gameManager;    // GameManager ��ũ��Ʈ ����

    private float blinkDuration = 0.5f; // ��¦�̴� ���� �ð�
    private float blinkInterval = 0.1f; // ��¦�̴� ����
    private bool isBlinking = false;    // ���� �����̴� ������ ����

    void Start()
    {
        // GameManager�� ã�Ƽ� �Ҵ�
        gameManager = FindObjectOfType<GameManager>();
        grayLevelUpButton.gameObject.SetActive(false); // �ʱ⿡�� ȸ�� ��ư ����
    }

    void Update()
    {
        // ������ ��ư�� ������ ������ Ŭ ���
        if (gameManager.money < gameManager.levelUpCost)
        {
            grayLevelUpButton.gameObject.SetActive(true); // ȸ�� ��ư ���̱�

            // ���� ������ ������ ���� �����̱� ����
            if (gameManager.money >= gameManager.levelUpCost && !isBlinking)
            {
                StartCoroutine(BlinkGrayButton());
            }
        }
        else
        {
            // ���� ����ϸ� ȸ�� ��ư�� ����
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

        for (int i = 0; i < 5; i++) // 5ȸ ������
        {
            grayLevelUpButton.gameObject.SetActive(true); // ȸ�� ��ư ���̱�
            yield return new WaitForSeconds(blinkDuration); // 0.5�� ���� ���̱�
            grayLevelUpButton.gameObject.SetActive(false); // ȸ�� ��ư �����
            yield return new WaitForSeconds(blinkInterval); // 0.1�� ���� �����
        }

        // �������� ���� �� ȸ�� ��ư�� ��� ����
        grayLevelUpButton.gameObject.SetActive(false); // ������ �� ����
        isBlinking = false;
    }

    public void OnLevelUpButtonClicked()
    {
        // ������ ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
        grayLevelUpButton.gameObject.SetActive(false); // ȸ�� ��ư �����
        StopAllCoroutines(); // ������ ����
        isBlinking = false;
    }
}
