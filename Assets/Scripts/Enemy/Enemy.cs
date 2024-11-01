using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;//�̵��ӵ�
    public float attackSpeed;//���ݼӵ�
    public float attackRange;//���ݹ���
    public float attackPower;//���ݷ�
    public float health;//ü��

    private Vector3 spwanPosition; //������ġ ���⿡ ���� �����Ұ���
    private float targetX; //x�����θ� �̵��Ұ���


    public void Initialize(Vector3 spwanPos)

    {  //���� ��������
        spwanPosition = spwanPos;
        //��ġ �̵�
        transform.position = spwanPosition;

        //�̵����� ���̴ϱ� player�� ���� �������� ������

        targetX = spwanPosition.x + 50f;
        //Ÿ���� ���� �����Ѵ�� ��������ġ���� x�����θ� 50��ǥ���� �̵��Ѵٴ� ����.
        void Update()
        {
            MoveTowardsTarget();//�������Ӹ��� �����δٴ°�
        }

        void MoveTowardsTarget()//���������� ����Ÿ��Ÿ���� ���� ���⿡ ����� �������� �����ִ°���

        {
            // ���� ��ġ���� ��ǥ ��ġ�� �̵�
            if (transform.position.x < targetX)
            {
                transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
        }






    }


















}
