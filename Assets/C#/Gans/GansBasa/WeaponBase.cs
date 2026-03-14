// БАЗА оружия , сюда не лезем просто наследуем от этого кода 
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float cooldown = 1f; // перезарядка
    protected float timer;

    protected Transform player;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Attack();
            timer = cooldown;
        }
    }

    protected virtual void Attack()
    {

    }

    protected Transform FindNearestEnemy(float radius)
    {
        GameObject[] враги = GameObject.FindGameObjectsWithTag("Vrag");

        Transform ближайший = null;
        float дистанция = Mathf.Infinity;

        foreach (GameObject враг in враги)
        {
            float d = Vector2.Distance(player.position, враг.transform.position);

            if (d < дистанция && d < radius)
            {
                дистанция = d;
                ближайший = враг.transform;
            }
        }

        return ближайший;
    }
}