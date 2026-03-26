using UnityEngine;

public class SkullWeapon : MonoBehaviour
{
    public GameObject skullPrefab;

    public int skullCount = 1;
    public float radius = 2f;
    public float speed = 180f;
    public int damage = 1;

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj == null)
        {
            Debug.LogError("Player не найден");
            return;
        }

        player = playerObj.transform;

        SpawnSkulls();
    }

    void SpawnSkulls()
    {
        if (skullPrefab == null)
        {
            Debug.LogError("skullPrefab не назначен");
            return;
        }

        if (player == null)
        {
            Debug.LogError("player не найден в SkullWeapon");
            return;
        }

        for (int i = 0; i < skullCount; i++)
        {
            GameObject skull = Instantiate(skullPrefab, player.position, Quaternion.identity);

            OrbitingSkull orbit = skull.GetComponent<OrbitingSkull>();

            if (orbit == null)
            {
                Debug.LogError("На Skull prefab нет скрипта OrbitingSkull");
                return;
            }

            orbit.player = player;
            orbit.radius = radius;
            orbit.speed = speed;
            orbit.damage = damage;
        }
    }

    public void UpgradeCount()
    {
        skullCount++;
        SpawnSkulls();
    }

    public void UpgradeDamage(int value)
    {
        damage += value;
    }
}