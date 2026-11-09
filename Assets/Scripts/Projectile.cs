
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;

    private Enemy target;
    private float damage;

    public void Initialize(Enemy newTarget, float newDamage)
    {
        target = newTarget;
        damage = newDamage;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.transform.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(
            transform.position,
            target.transform.position) < 0.15f)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
