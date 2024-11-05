
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public float playerMoveSpeed; // 이동 속도
    public int attackSpeed; // 공격 속도
    public float attackRange; // 공격 범위
    public int attackPower; // 공격력
    public int health; // 체력

    public Vector3 spawnPlayerPosition; // 스폰 위치
    public float playerTargetX; // 목표 x 위치

    public void Initialize(Vector3 spawnPos)
    {
        spawnPlayerPosition = spawnPos;
        transform.position = spawnPlayerPosition;

        // 목표 위치 설정 (x축으로만 50 단위 이동)
        playerTargetX = spawnPlayerPosition.x + 100f;
    }

    private void Update()
    {
        MoveTowardsPlayerTarget(); // 매 프레임마다 이동
    }

    private void MoveTowardsPlayerTarget()
    {
        // 현재 위치에서 목표 위치로 이동
        if (transform.position.x < playerTargetX)
        {
            transform.position += new Vector3(playerMoveSpeed * Time.deltaTime, 0, 0);
        }
    }
}