using UnityEngine;

public class Enemy : Unit
{
    void Start()
    {
        isEnemy = true;  // "���� ���̾�!" ��� ǥ��
        Initialize(transform.position);
    }
}