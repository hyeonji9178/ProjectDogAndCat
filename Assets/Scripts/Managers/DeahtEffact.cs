using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public float moveSpeed = 3f;  // Y�� �̵� �ӵ�
    public float fadeSpeed = 1f;  // ���̵� �ƿ� �ӵ�
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 1�� �Ŀ� ����Ʈ ������Ʈ ����
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        // ���� �������� �̵�
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);

        // ���̵� �ƿ� ȿ��
        Color color = spriteRenderer.color;
        color.a -= fadeSpeed * Time.deltaTime;
        spriteRenderer.color = color;
    }
}