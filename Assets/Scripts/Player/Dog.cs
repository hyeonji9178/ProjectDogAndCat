using UnityEngine;

public class Dog : Unit
{
    void Start()
    {
        isEnemy = false;  // 아군 설정
        Initialize(transform.position);
    }
}