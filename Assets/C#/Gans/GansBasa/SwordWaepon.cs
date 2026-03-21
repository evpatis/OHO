using UnityEngine;

public class SwordWeapon : WeaponBase
{
    public float attackRadius = 1.5f;
    public int damage = 2;

    [Header("Визуал для теста")]
    public GameObject hitVisualPrefab;
    public float visualDistance = 1f;

    protected override void Attack()
    {
        if (player == null) return;

        Vector3 direction = Vector3.right;

        if (player.localScale.x < 0)
            direction = Vector3.left;

        Vector3 hitPosition = player.position + direction * visualDistance;

        if (hitVisualPrefab != null)
        {
            Instantiate(hitVisualPrefab, hitPosition, Quaternion.identity);
        }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(hitPosition, attackRadius);

        foreach (Collider2D enemyCol in enemies)
        {
            if (!enemyCol.CompareTag("Vrag")) continue;

            Vrag enemy = enemyCol.GetComponent<Vrag>();
            if (enemy != null)
            {
                enemy.ПолучитьУрон(damage);
            }
        }

        Debug.Log("Удар второго оружия");
    }
}