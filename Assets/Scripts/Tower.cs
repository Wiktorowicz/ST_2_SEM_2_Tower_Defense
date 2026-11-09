
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 4f;
    public float damage = 25f;
    public float fireRate = 1f;

    public GameObject projectilePrefab;
    public Transform projectileParent;

    public LayerMask enemyLayer;

    private float fireTimer;

    private void Update()
    {
        fireTimer -= Time.deltaTime;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position,
            range,
            enemyLayer
        );

        Enemy target = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D enemyCollider in enemies)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();

            if (enemy == null)
                continue;

            float distance = Vector2.Distance(
                transform.position,
                enemy.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy;
            }
        }

        if (target != null && fireTimer <= 0f)
        {
            Shoot(target);
            fireTimer = 1f / fireRate;
        }
    }

    private void Shoot(Enemy target)
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity,
            projectileParent
        );

        Projectile projectileScript =
            projectile.GetComponent<Projectile>();

        projectileScript.Initialize(target, damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
