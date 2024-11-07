using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public float moveSpeed = 3f;  // Y축 이동 속도

    void Update()
    {
        // 위쪽 방향으로 이동
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    void Start()
    {
        // 1초 후에 이펙트 오브젝트 삭제
        Destroy(gameObject, 1f);
    }
}