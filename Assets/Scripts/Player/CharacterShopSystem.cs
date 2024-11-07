using UnityEngine;
using UnityEngine.UI;

public class CharacterShopSystem : MonoBehaviour
{
    public GameManager gameManager;      // 기존 GameManager 연결
    public PlayerSpawner prefabSpawner; // 기존 PrefabSpawner 연결

    // 각 캐릭터의 고정 가격
    private const int DOG1_PRICE = 50;
    private const int DOG2_PRICE = 100;
    private const int DOG3_PRICE = 200;
    private const int DOG4_PRICE = 400;
    private const int DOG5_PRICE = 400;

    // 버튼들
    public Button dog1Button;
    public Button dog2Button;
    public Button dog3Button;
    public Button dog4Button;
    public Button dog5Button;

    void Start()
    {
        // 버튼에 구매 함수 연결
        dog1Button.onClick.AddListener(() => TryBuyCharacter(0, DOG1_PRICE));
        dog2Button.onClick.AddListener(() => TryBuyCharacter(1, DOG2_PRICE));
        dog3Button.onClick.AddListener(() => TryBuyCharacter(2, DOG3_PRICE));
        dog4Button.onClick.AddListener(() => TryBuyCharacter(3, DOG4_PRICE));
        dog5Button.onClick.AddListener(() => TryBuyCharacter(4, DOG5_PRICE));
    }

    void Update()
    {
        // 버튼 활성화/비활성화 상태 업데이트
        UpdateButtonStates();
    }

    void UpdateButtonStates()
    {
        dog1Button.interactable = gameManager.money >= DOG1_PRICE;
        dog2Button.interactable = gameManager.money >= DOG2_PRICE;
        dog3Button.interactable = gameManager.money >= DOG3_PRICE;
        dog4Button.interactable = gameManager.money >= DOG4_PRICE;
        dog5Button.interactable = gameManager.money >= DOG5_PRICE;
    }

    void TryBuyCharacter(int characterIndex, int price)
    {
        if (gameManager.money >= price)
        {
            // 돈 차감
            gameManager.money -= price;

            // 캐릭터 생성
            prefabSpawner.SpawnPrefab(characterIndex);
        }
    }
}