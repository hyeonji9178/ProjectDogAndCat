using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public float moveSpeed = 3f;  // Y축 이동 속도
    public float fadeSpeed = 1f;  // 페이드 아웃 속도
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 1초 후에 이펙트 오브젝트 삭제
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        // 위쪽 방향으로 이동
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);

        // 페이드 아웃 효과
        Color color = spriteRenderer.color;
        color.a -= fadeSpeed * Time.deltaTime;
        spriteRenderer.color = color;
    }
}