using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Basic Stats")]
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    public bool isEnemy;

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

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        Initialize(transform.position);
    }

    public void Initialize(Vector3 spawnPos)
    {
        transform.position = spawnPos;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentTarget == null)
        {
            Move();
            FindTarget(); // ��� Ÿ�� ã��
        }
        else
        {
            // Ÿ�ٰ��� �Ÿ� üũ
            float distance = Vector2.Distance(transform.position, currentTarget.transform.position);

            if (distance > attackRange)
            {
                // ���� �������� �־����� �ٽ� �̵�
                currentTarget = null;
                Move();
            }
            else if (Time.time >= lastAttackTime + attackSpeed)
            {
                Attack();
            }
        }
    }

    void Move()
    {
        float direction = isEnemy ? -1 : 1;
        transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
    }

    void FindTarget()
    {
        // ���� ���� ���� ��� �ݶ��̴� ����
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, targetLayers);

        float nearestDistance = float.MaxValue;
        GameObject nearestTarget = null;
        GameObject nearestBase = null;

        foreach (Collider2D hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);

            if (isEnemy)
            {
                // �� ������ ���
                if (hit.CompareTag("PlayerUnit"))
                {
                    // �÷��̾� ������ �ֿ켱
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestTarget = hit.gameObject;
                    }
                }
                else if (hit.CompareTag("PlayerBase") && nearestTarget == null)
                {
                    // �÷��̾� ������ ������ ���� ���� Ÿ��
                    nearestBase = hit.gameObject;
                }
            }
            else
            {
                // �Ʊ� ������ ���
                if (hit.CompareTag("EnemyUnit"))
                {
                    // �� ������ �ֿ켱
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestTarget = hit.gameObject;
                    }
                }
                else if (hit.CompareTag("EnemyBase") && nearestTarget == null)
                {
                    // �� ������ ������ ���� ���� Ÿ��
                    nearestBase = hit.gameObject;
                }
            }
        }

        // Ÿ�� ����: ������ ������ ������, ������ ������
        currentTarget = nearestTarget != null ? nearestTarget : nearestBase;
    }

    void Attack()
    {
        if (isAreaAttack)
        {
            // ���� ����
            Collider2D[] hits = Physics2D.OverlapCircleAll(currentTarget.transform.position, areaRadius, targetLayers);
            foreach (Collider2D hit in hits)
            {
                ApplyDamage(hit.gameObject);
            }
        }
        else
        {
            // ���� Ÿ�� ����
            ApplyDamage(currentTarget);
        }

        lastAttackTime = Time.time;
    }

    void ApplyDamage(GameObject target)
    {
        Unit enemyUnit = target.GetComponent<Unit>();
        if (enemyUnit != null)
        {
            enemyUnit.TakeDamage(attackPower);
        }

        BaseHealth baseHealth = target.GetComponent<BaseHealth>();
        if (baseHealth != null)
        {
            baseHealth.TakeDamage(attackPower);
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

    // ���� ������ �ð������� ǥ�� (����׿�)
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