using UnityEngine;

public class SwordWeapon : WeaponBase
{
    public float attackRadius = 1.5f;
    public int damage = 2;

    protected override void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(player.position, attackRadius);

        foreach (Collider2D enemyCol in enemies)
        {
            if (!enemyCol.CompareTag("Vrag")) continue;

            Vrag enemy = enemyCol.GetComponent<Vrag>();
            if (enemy != null)
            {
                enemy.ПолучитьУрон(damage);
            }
        }

        Debug.Log("Меч ударил");
    }
}