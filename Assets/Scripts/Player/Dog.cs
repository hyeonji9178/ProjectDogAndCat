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
        // ������ �� �ڵ����� Initialize ȣ��
        Initialize(transform.position);
    }

    public void Initialize(Vector3 spawnPos)
    {
        spawnPlayerPosition = spawnPos;
        transform.position = spawnPlayerPosition;

        // ���� ��ġ���� �������� 100 �̵��ϵ��� ����
        playerTargetX = spawnPlayerPosition.x - 100f;
    }

    private void Update()
    {
        MoveTowardsPlayerTarget();
    }

    private void MoveTowardsPlayerTarget()
    {
        // ���� ��ġ�� ��ǥ���� ũ��(�����ʿ� ������) �������� �̵�
        if (transform.position.x > playerTargetX)
        {
            transform.position += new Vector3(-playerMoveSpeed * Time.deltaTime, 0, 0);
        }
    }
}

