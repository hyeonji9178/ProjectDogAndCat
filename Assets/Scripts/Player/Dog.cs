
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public float playerMoveSpeed; // �̵� �ӵ�
    public int attackSpeed; // ���� �ӵ�
    public float attackRange; // ���� ����
    public int attackPower; // ���ݷ�
    public int health; // ü��

    public Vector3 spawnPlayerPosition; // ���� ��ġ
    public float playerTargetX; // ��ǥ x ��ġ

    public void Initialize(Vector3 spawnPos)
    {
        spawnPlayerPosition = spawnPos;
        transform.position = spawnPlayerPosition;

        // ��ǥ ��ġ ���� (x�����θ� 50 ���� �̵�)
        playerTargetX = spawnPlayerPosition.x + 100f;
    }

    private void Update()
    {
        MoveTowardsPlayerTarget(); // �� �����Ӹ��� �̵�
    }

    private void MoveTowardsPlayerTarget()
    {
        // ���� ��ġ���� ��ǥ ��ġ�� �̵�
        if (transform.position.x < playerTargetX)
        {
            transform.position += new Vector3(playerMoveSpeed * Time.deltaTime, 0, 0);
        }
    }
}