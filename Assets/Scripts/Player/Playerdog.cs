using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdog : MonoBehaviour
{
    public float moveSpeed = 5f;  // �̵� �ӵ�
    public int attackSpeed;
    public float attackRange;
    public int attackPower;
    public int health;

    public Vector3 spawnPosition;  // �ʱ� ���� ��ġ
    public float targetX = -50f;  // ��ǥ x�� ��ġ

    private bool isMoving = false; // �̵� ���� Ȯ�ο� ����

    private void Start()
    {
        // ĳ���Ͱ� ó�� ������ ��ġ ����
        transform.position = spawnPosition;
    }

    private void Update()
    {
        // �����̽� Ű�� ������ ���� �� �̵� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnAndMove();
        }

        // ��ǥ ��ġ�� �̵�
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    // ĳ���͸� �����ϰ� �̵��� �����ϴ� �Լ�
    private void SpawnAndMove()
    {
        // ĳ���͸� ���� ��ġ�� �����ϰ� �̵� ����
        transform.position = spawnPosition;
        isMoving = true;
    }

    // ĳ���͸� targetX �������� �̵���Ű�� �Լ�
    private void MoveToTarget()
    {
        // x�� ��ǥ ��ġ���� �̵�
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);

        // ��ǥ ��ġ�� �����ϸ� �̵� ����
        if (transform.position.x <= targetX)
        {
            isMoving = false;
        }
    }
}
