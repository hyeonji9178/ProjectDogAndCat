using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Basic Stats")]
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    public bool isEnemy;
    public int cost = 100;    // 생산 비용

    [Header("Attack Stats")]
    public int attackPower = 10;
    public float attackSpeed = 1f;
    public float attackRange = 2f;    // 공격 사정거리
    public bool isAreaAttack = false; // true면 범위공격, false면 단일공격
    public float areaRadius = 1f;     // 범위공격시 범위

    [Header("Current Status")]
    public int currentHealth;
    private float lastAttackTime;
    private GameObject currentTarget;

    [Header("Target Layers")]
    public LayerMask targetLayers;     // 공격 가능한 레이어

    // 기지 위치 상수
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
        // 1. 먼저 타겟 찾기
        FindTarget();

        // 2. 타겟이 있고 공격 범위 안에 있으면 공격
        if (currentTarget != null)
        {
            float distance = Vector2.Distance(transform.position, currentTarget.transform.position);
            if (distance <= attackRange)
            {
                // 공격 범위 안이면 공격
                if (Time.time >= lastAttackTime + attackSpeed)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                // 공격 범위 밖이면 이동
                Move();
            }
        }
        else
        {
            // 타겟이 없으면 이동
            Move();
        }
    }

    void Move()
    {
        if (currentTarget == null)
        {
            // 타겟이 없을 때는 기지 방향으로 이동 (X축만)
            float direction = isEnemy ? 1 : -1;
            float targetX = isEnemy ? RIGHT_BASE_X : LEFT_BASE_X;

            // 기지 위치를 넘어가지 않도록 체크
            if ((isEnemy && transform.position.x < targetX) ||
                (!isEnemy && transform.position.x > targetX))
            {
                transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            // 타겟이 있을 때도 X축으로만 이동
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
            // 범위 공격
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
            // 단일 타겟 공격
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