using UnityEngine;

public class AuraWeapon : WeaponBase
{
    public float radius = 2f;
    public int damage = 1;

    [Header("Визуал")]
    public GameObject auraVisualPrefab;

    private GameObject currentAuraVisual;

    protected override void Start()
    {
        base.Start();

        if (auraVisualPrefab != null && currentAuraVisual == null)
        {
            currentAuraVisual = Instantiate(auraVisualPrefab, player.position, Quaternion.identity);
        }
    }

    private void OnEnable()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        if (auraVisualPrefab != null && currentAuraVisual == null && player != null)
        {
            currentAuraVisual = Instantiate(auraVisualPrefab, player.position, Quaternion.identity);
        }

        if (currentAuraVisual != null)
            currentAuraVisual.SetActive(true);
    }

    private void OnDisable()
    {
        if (currentAuraVisual != null)
            currentAuraVisual.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        if (currentAuraVisual != null && player != null)
        {
            currentAuraVisual.transform.position = player.position;
        }
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
}