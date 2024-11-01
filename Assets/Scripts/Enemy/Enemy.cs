using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;//이동속도
    public float attackSpeed;//공격속도
    public float attackRange;//공격범위
    public float attackPower;//공격력
    public float health;//체력

    private Vector3 spwanPosition; //스폰위치 여기에 범위 지정할거임
    private float targetX; //x축으로만 이동할거임


    public void Initialize(Vector3 spwanPos)

    {  //값을 넣을거임
        spwanPosition = spwanPos;
        //위치 이동
        transform.position = spwanPosition;

        //이동방향 적이니까 player의 기지 방향으로 갈거임

        targetX = spwanPosition.x + 50f;
        //타겟은 위에 선언한대로 스폰된위치에서 x축으로만 50좌표까지 이동한다는 뜻임.
        void Update()
        {
            MoveTowardsTarget();//매프레임마다 움직인다는것
        }

        void MoveTowardsTarget()//위에선언한 무브타워타겟을 이제 여기에 어더한 동작인지 적어주는거임

        {
            // 현재 위치에서 목표 위치로 이동
            if (transform.position.x < targetX)
            {
                transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
        }






    }


















}
