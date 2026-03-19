using UnityEngine;

public class KnifeWeapon : WeaponBase
{
    public float attackRadius = 1.2f;
    public int damage = 2;
    public float attackDistance = 1f;

    public GameObject hitVisualPrefab;

    protected override void Attack()
    {
        if (player == null) return;

        SpriteRenderer playerSprite = player.GetComponentInChildren<SpriteRenderer>();
        if (playerSprite == null) return;

        Vector2 direction = playerSprite.flipX ? Vector2.left : Vector2.right;
        Vector2 hitPosition = (Vector2)player.position + direction * attackDistance;

        if (hitVisualPrefab != null)
        {
            GameObject hit = Instantiate(hitVisualPrefab, hitPosition, Quaternion.identity);

            Vector3 scale = hit.transform.localScale;
            scale.x = playerSprite.flipX ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            hit.transform.localScale = scale;
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

        Debug.Log("Нож ударил");
    }
}