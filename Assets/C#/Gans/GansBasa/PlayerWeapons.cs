using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public KnifeWeapon knifeWeapon;
    public MagicWeapon magicWeapon;

    private void Start()
    {
        Debug.Log("PlayerWeapons Start");

        if (knifeWeapon != null)
        {
            knifeWeapon.enabled = true;
            Debug.Log("KnifeWeapon включен");
        }

        if (magicWeapon != null)
        {
            magicWeapon.enabled = false;
            Debug.Log("MagicWeapon выключен на старте");
        }
        else
        {
            Debug.LogError("magicWeapon НЕ назначен в PlayerWeapons");
        }
    }

    public void UnlockMagic()
    {
        Debug.Log("UnlockMagic вызван");

        if (magicWeapon == null)
        {
            Debug.LogError("MagicWeapon не назначен");
            return;
        }

        magicWeapon.enabled = true;
        Debug.Log("Магия включена. enabled = " + magicWeapon.enabled);
    }

    public void UpgradeKnifeDamage(int amount)
    {
        if (knifeWeapon != null)
        {
            knifeWeapon.damage += amount;
            Debug.Log("Урон ножа увеличен");
        }
    }
}