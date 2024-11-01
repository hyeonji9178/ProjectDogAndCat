using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed; // �̵� �ӵ�
    public float attackSpeed; // ���� �ӵ�
    public float attackRange; // ���� ����
    public float attackPower; // ���ݷ�
    public float health; // ü��

    private Vector3 spawnPosition; // ���� ��ġ
    private float targetX; // ��ǥ x ��ġ

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
