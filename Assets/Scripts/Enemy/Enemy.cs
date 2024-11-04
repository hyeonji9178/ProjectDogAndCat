using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed; // �̵� �ӵ�
    public int attackSpeed; // ���� �ӵ�
    public float attackRange; // ���� ����
    public int attackPower; // ���ݷ�
    public int health; // ü��

    public Vector3 spawnPosition; // ���� ��ġ
    public float targetX; // ��ǥ x ��ġ

    public void Initialize(Vector3 spawnPos)
    {
        spawnPosition = spawnPos;
        transform.position = spawnPosition;

        // ��ǥ ��ġ ���� (x�����θ� 50 ���� �̵�)
        targetX = spawnPosition.x + 100f;
    }

    private void Update()
    {
        MoveTowardsTarget(); // �� �����Ӹ��� �̵�
    }

    private void MoveTowardsTarget()
    {
        // ���� ��ġ���� ��ǥ ��ġ�� �̵�
        if (transform.position.x < targetX)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
    }
}
