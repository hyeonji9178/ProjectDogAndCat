using UnityEngine;
using UnityEngine.UI;

public class CharacterShopSystem : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerSpawner prefabSpawner;

    private const int DOG1_PRICE = 50;
    private const int DOG2_PRICE = 100;
    private const int DOG3_PRICE = 200;
    private const int DOG4_PRICE = 400;
    private const int DOG5_PRICE = 400;

    public Button dog1Button;
    public Button dog2Button;
    public Button dog3Button;
    public Button dog4Button;
    public Button dog5Button;

    public SpawnCooldown[] spawnCooldowns;

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
        UpdateButtonStates();
    }

    void UpdateButtonStates()
    {
        // IsOnCooldown 사용
        dog1Button.interactable = gameManager.money >= DOG1_PRICE && !spawnCooldowns[0].IsOnCooldown();
        dog2Button.interactable = gameManager.money >= DOG2_PRICE && !spawnCooldowns[1].IsOnCooldown();
        dog3Button.interactable = gameManager.money >= DOG3_PRICE && !spawnCooldowns[2].IsOnCooldown();
        dog4Button.interactable = gameManager.money >= DOG4_PRICE && !spawnCooldowns[3].IsOnCooldown();
        dog5Button.interactable = gameManager.money >= DOG5_PRICE && !spawnCooldowns[4].IsOnCooldown();
    }

    void TryBuyCharacter(int characterIndex, int price)
    {
        // IsOnCooldown 사용
        if (gameManager.money >= price && !spawnCooldowns[characterIndex].IsOnCooldown())
        {
            gameManager.money -= price;
            prefabSpawner.SpawnPrefab(characterIndex);
            spawnCooldowns[characterIndex].isOnCooldown = true;
            spawnCooldowns[characterIndex].StartCooldown();
        }
    }
}