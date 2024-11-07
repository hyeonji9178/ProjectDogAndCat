using UnityEngine;

public class Enemy : Unit
{
    void Start()
    {
        isEnemy = true;  // "나는 적이야!" 라고 표시
        Initialize(transform.position);
    }
}