using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public float moveSpeed = 3f;  // Y�� �̵� �ӵ�

    void Update()
    {
        // ���� �������� �̵�
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    void Start()
    {
        // 1�� �Ŀ� ����Ʈ ������Ʈ ����
        Destroy(gameObject, 1f);
    }
}