using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Basic Stats")]
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    public bool isEnemy;
    public int cost = 100;    // ���� ���

    [Header("Attack Stats")]
    public int attackPower = 10;
    public float attackSpeed = 1f;
    public float attackRange = 2f;    // ���� �����Ÿ�
    public bool isAreaAttack = false; // true�� ��������, false�� ���ϰ���
    public float areaRadius = 1f;     // �������ݽ� ����

    [Header("Current Status")]
    public int currentHealth;
    private float lastAttackTime;
    private GameObject currentTarget;

    [Header("Target Layers")]
    public LayerMask targetLayers;     // ���� ������ ���̾�

    // ���� ��ġ ���
    private const float RIGHT_BASE_X = 50f;
    private const float LEFT_BASE_X = -50f;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        Initialize(transform.position);
    }

    public void Initialize(Vector3 spawnPos)
    {
        transform.position = spawnPos;
        currentHealth = maxHealth;
        lastAttackTime = 0f;
    }

    void Update()
    {
        // 1. ���� Ÿ�� ã��
        FindTarget();

        // 2. Ÿ���� �ְ� ���� ���� �ȿ� ������ ����
        if (currentTarget != null)
        {
            float distance = Vector2.Distance(transform.position, currentTarget.transform.position);
            if (distance <= attackRange)
            {
                // ���� ���� ���̸� ����
                if (Time.time >= lastAttackTime + attackSpeed)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                // ���� ���� ���̸� �̵�
                Move();
            }
        }
        else
        {
            // Ÿ���� ������ �̵�
            Move();
        }
    }

    void Move()
    {
        if (currentTarget == null)
        {
            // Ÿ���� ���� ���� ���� �������� �̵� (X�ุ)
            float direction = isEnemy ? 1 : -1;
            float targetX = isEnemy ? RIGHT_BASE_X : LEFT_BASE_X;

            // ���� ��ġ�� �Ѿ�� �ʵ��� üũ
            if ((isEnemy && transform.position.x < targetX) ||
                (!isEnemy && transform.position.x > targetX))
            {
                transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            // Ÿ���� ���� ���� X�����θ� �̵�
            float direction = currentTarget.transform.position.x > transform.position.x ? 1 : -1;
            transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, targetLayers);

        float nearestDistance = float.MaxValue;
        GameObject nearestTarget = null;

        foreach (Collider2D hit in hits)
        {
            if (hit == null) continue;

            float distance = Vector2.Distance(transform.position, hit.transform.position);

            if (isEnemy)
            {
                if (hit.CompareTag("PlayerUnit") || hit.CompareTag("PlayerBase"))
                {
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestTarget = hit.gameObject;
                    }
                }
            }
            else
            {
                if (hit.CompareTag("EnemyUnit") || hit.CompareTag("EnemyBase"))
                {
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestTarget = hit.gameObject;
                    }
                }
            }
        }

        currentTarget = nearestTarget;
    }

    void Attack()
    {
        if (currentTarget == null) return;

        if (isAreaAttack)
        {
            // ���� ����
            Collider2D[] hits = Physics2D.OverlapCircleAll(currentTarget.transform.position, areaRadius, targetLayers);
            foreach (Collider2D hit in hits)
            {
                if (hit.GetComponent<Unit>())
                {
                    hit.GetComponent<Unit>().TakeDamage(attackPower);
                }
                else if (hit.GetComponent<BaseHealth>())
                {
                    hit.GetComponent<BaseHealth>().TakeDamage(attackPower);
                }
            }
        }
        else
        {
            // ���� Ÿ�� ����
            if (currentTarget.GetComponent<Unit>())
            {
                currentTarget.GetComponent<Unit>().TakeDamage(attackPower);
            }
            else if (currentTarget.GetComponent<BaseHealth>())
            {
                currentTarget.GetComponent<BaseHealth>().TakeDamage(attackPower);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effect = Instantiate(Resources.Load<GameObject>("DeathEffect"), transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (isAreaAttack && currentTarget != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(currentTarget.transform.position, areaRadius);
        }
    }
} 