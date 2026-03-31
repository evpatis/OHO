using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Основные статы")]
    public int maxHP = 10;
    public int currentHP = 10;

    public int maxArmor = 0;
    public int currentArmor = 0;

    public float moveSpeed = 5f;

    [Header("Модификаторы")]
    public float damageMultiplier = 1f;
    public float attackSpeedMultiplier = 1f;

    public void AddHealth(int amount)
    {
        maxHP += amount;
        currentHP += amount;
        Debug.Log("HP увеличено: " + maxHP);
    }

    public void AddSpeed(float amount)
    {
        moveSpeed += amount;
        Debug.Log("Скорость: " + moveSpeed);
    }

    public void AddArmor(int amount)
    {
        maxArmor += amount;
        currentArmor += amount;
        Debug.Log("Броня увеличена: " + maxArmor);
    }

    public void AddDamageMultiplier(float amount)
    {
        damageMultiplier += amount;
    }

    public void AddAttackSpeed(float amount)
    {
        attackSpeedMultiplier += amount;
    }
}