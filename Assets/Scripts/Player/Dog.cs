using UnityEngine;

public class Dog : Unit
{
    void Start()
    {
        isEnemy = false;  // "���� �Ʊ��̾�!" ��� ǥ��
        Initialize(transform.position);
    }
}

