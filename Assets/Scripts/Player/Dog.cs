using UnityEngine;

public class Dog : Unit
{
    void Start()
    {
        isEnemy = false;  // �Ʊ� ����
        Initialize(transform.position);
    }
}