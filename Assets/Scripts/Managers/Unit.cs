using UnityEngine;

public class Unit : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int attackPower = 10;
    public float attackSpeed = 1f;
    public int health = 100;
    public bool isEnemy;

    private float lastAttackTime;
    private GameObject currentTarget;

    protected virtual void Start()
    {
        Initialize(transform.position);
    }

    public void Initialize(Vector3 spawnPos)
    {
        transform.position = spawnPos;
    }

    void Update()
    {
        if (currentTarget == null)
        {
            Move();
        }
        else if (Time.time >= lastAttackTime + attackSpeed)
        {
            Attack();
        }
    }

    void Move()
    {
        float direction = isEnemy ? -1 : 1;
        transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
    }

    void Attack()
    {
        if (currentTarget != null)
        {
            Unit enemyUnit = currentTarget.GetComponent<Unit>();
            if (enemyUnit != null)
            {
                enemyUnit.TakeDamage(attackPower);
            }

            BaseHealth baseHealth = currentTarget.GetComponent<BaseHealth>();
            if (baseHealth != null)
            {
                baseHealth.TakeDamage(attackPower);
            }

            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentTarget == null)
        {
            if (isEnemy && (other.CompareTag("PlayerUnit") || other.CompareTag("PlayerBase")))
            {
                currentTarget = other.gameObject;
            }
            else if (!isEnemy && (other.CompareTag("EnemyUnit") || other.CompareTag("EnemyBase")))
            {
                currentTarget = other.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentTarget != null && other.gameObject == currentTarget.gameObject)
        {
            currentTarget = null;
        }
    }
}