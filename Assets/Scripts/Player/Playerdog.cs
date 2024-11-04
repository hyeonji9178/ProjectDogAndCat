using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdog : MonoBehaviour
{
    public float moveSpeed = 5f;  // 이동 속도
    public int attackSpeed;
    public float attackRange;
    public int attackPower;
    public int health;

    public Vector3 spawnPosition;  // 초기 스폰 위치
    public float targetX = -50f;  // 목표 x축 위치

    private bool isMoving = false; // 이동 여부 확인용 변수

    private void Start()
    {
        // 캐릭터가 처음 생성될 위치 지정
        transform.position = spawnPosition;
    }

    private void Update()
    {
        // 스페이스 키를 누르면 스폰 및 이동 시작
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnAndMove();
        }

        // 목표 위치로 이동
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    // 캐릭터를 스폰하고 이동을 시작하는 함수
    private void SpawnAndMove()
    {
        // 캐릭터를 스폰 위치에 생성하고 이동 시작
        transform.position = spawnPosition;
        isMoving = true;
    }

    // 캐릭터를 targetX 방향으로 이동시키는 함수
    private void MoveToTarget()
    {
        // x축 목표 위치까지 이동
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);

        // 목표 위치에 도달하면 이동 종료
        if (transform.position.x <= targetX)
        {
            isMoving = false;
        }
    }
}
