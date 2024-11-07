using UnityEngine;

public class Dog : MonoBehaviour
{
    public float playerMoveSpeed;
    public int attackSpeed;
    public float attackRange;
    public int attackPower;
    public int health;

    public Vector3 spawnPlayerPosition;
    public float playerTargetX;

    private void Start()
    {
        // 시작할 때 자동으로 Initialize 호출
        Initialize(transform.position);
    }

    public void Initialize(Vector3 spawnPos)
    {
        spawnPlayerPosition = spawnPos;
        transform.position = spawnPlayerPosition;

        // 현재 위치에서 왼쪽으로 100 이동하도록 설정
        playerTargetX = spawnPlayerPosition.x - 100f;
    }

    private void Update()
    {
        MoveTowardsPlayerTarget();
    }

    private void MoveTowardsPlayerTarget()
    {
        // 현재 위치가 목표보다 크면(오른쪽에 있으면) 왼쪽으로 이동
        if (transform.position.x > playerTargetX)
        {
            transform.position += new Vector3(-playerMoveSpeed * Time.deltaTime, 0, 0);
        }
    }
}

