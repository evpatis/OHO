using UnityEngine;

public class AuraWeapon : WeaponBase
{
    public float radius = 2f;
    public int damage = 1;

    [Header("Визуал")]
    public GameObject auraVisual;

    protected override void Start()
    {
        base.Start();

        if (auraVisual != null)
            auraVisual.SetActive(true);
    }

    protected override void Attack()
    {
        if (player == null) return;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(player.position, radius);

        foreach (Collider2D enemyCol in enemies)
        {
            if (!enemyCol.CompareTag("Vrag")) continue;

            Vrag enemy = enemyCol.GetComponent<Vrag>();
            if (enemy != null)
            {
                enemy.ПолучитьУрон(damage);
            }
        }

        Debug.Log("Аура нанесла урон");
    }

    private void Update()
    {
        base.Update();

        if (auraVisual != null && player != null)
        {
            auraVisual.transform.position = player.position;
        }
    }
}
