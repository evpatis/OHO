// авто стрельба сюда не лазить! 
using UnityEngine;

public class AutoTargetWeapon : WeaponBase
{
    public float радиусПоиска = 15f;

    protected override void Attack()
    {
        Transform цель = FindNearestEnemy(радиусПоиска);

        if (цель == null) return;

        Vector2 направление = (цель.position - player.position).normalized;

        GameObject bullet = Instantiate(projectilePrefab, player.position, Quaternion.identity);

        ProjectileBase proj = bullet.GetComponent<ProjectileBase>();

        if (proj != null)
            proj.Инициализация(направление);
    }
}