using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 3;

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
