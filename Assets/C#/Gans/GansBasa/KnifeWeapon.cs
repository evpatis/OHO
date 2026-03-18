// пуля в ближайшего врага 
using UnityEngine;

public class KnifeWeapon : WeaponBase
{
    public float attackRadius = 10f;
    public Transform firePoint;
    public int damage = 1;

    protected override void Attack()
    {
        Transform enemy = FindNearestEnemy(attackRadius);
        if (enemy == null) return;
        if (projectilePrefab == null) return;
        if (firePoint == null) return;

        Vector2 dir = (enemy.position - firePoint.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        ProjectileBase projectileScript = projectile.GetComponent<ProjectileBase>();
        if (projectileScript != null)
        {
            projectileScript.урон = damage;
            projectileScript.Инициализация(dir);
        }
    }
}