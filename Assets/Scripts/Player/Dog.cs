using UnityEngine;

public class Dog : Unit
{
    void Start()
    {
        isEnemy = false;  // "나는 아군이야!" 라고 표시
        Initialize(transform.position);
    }
}

