using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed; // 이동 속도
    public int attackSpeed; // 공격 속도
    public float attackRange; // 공격 범위
    public int attackPower; // 공격력
    public int health; // 체력

    public Vector3 spawnPosition; // 스폰 위치
    public float targetX; // 목표 x 위치

    public void Initialize(Vector3 spawnPos)
    {
        spawnPosition = spawnPos;
        transform.position = spawnPosition;

        // 목표 위치 설정 (x축으로만 50 단위 이동)
        targetX = spawnPosition.x + 100f;
    }

    private void Update()
    {
        MoveTowardsTarget(); // 매 프레임마다 이동
    }

    private void MoveTowardsTarget()
    {
        // 현재 위치에서 목표 위치로 이동
        if (transform.position.x < targetX)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
    }
}
