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
    public float attackRange = 2f;    // 공격 사정거리
    public bool isAreaAttack = false; // true면 범위공격, false면 단일공격
    public float areaRadius = 1f;     // 범위공격시 범위

    [Header("Current Status")]
    public int currentHealth;
    private float lastAttackTime;
    private GameObject currentTarget;

    [Header("Target Layers")]
    public LayerMask targetLayers;     // 공격 가능한 레이어

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
            FindTarget(); // 계속 타겟 찾기
        }
        else
        {
            // 타겟과의 거리 체크
            float distance = Vector2.Distance(transform.position, currentTarget.transform.position);

            if (distance > attackRange)
            {
                // 공격 범위보다 멀어지면 다시 이동
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
        // 공격 범위 내의 모든 콜라이더 검출
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, targetLayers);

        float nearestDistance = float.MaxValue;
        GameObject nearestTarget = null;
        GameObject nearestBase = null;

        foreach (Collider2D hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);

            if (isEnemy)
            {
                // 적 유닛의 경우
                if (hit.CompareTag("PlayerUnit"))
                {
                    // 플레이어 유닛이 최우선
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestTarget = hit.gameObject;
                    }
                }
                else if (hit.CompareTag("PlayerBase") && nearestTarget == null)
                {
                    // 플레이어 기지는 유닛이 없을 때만 타겟
                    nearestBase = hit.gameObject;
                }
            }
            else
            {
                // 아군 유닛의 경우
                if (hit.CompareTag("EnemyUnit"))
                {
                    // 적 유닛이 최우선
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestTarget = hit.gameObject;
                    }
                }
                else if (hit.CompareTag("EnemyBase") && nearestTarget == null)
                {
                    // 적 기지는 유닛이 없을 때만 타겟
                    nearestBase = hit.gameObject;
                }
            }
        }

        // 타겟 설정: 유닛이 있으면 유닛을, 없으면 기지를
        currentTarget = nearestTarget != null ? nearestTarget : nearestBase;
    }

    void Attack()
    {
        if (isAreaAttack)
        {
            // 범위 공격
            Collider2D[] hits = Physics2D.OverlapCircleAll(currentTarget.transform.position, areaRadius, targetLayers);
            foreach (Collider2D hit in hits)
            {
                ApplyDamage(hit.gameObject);
            }
        }
        else
        {
            // 단일 타겟 공격
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

    // 공격 범위를 시각적으로 표시 (디버그용)
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