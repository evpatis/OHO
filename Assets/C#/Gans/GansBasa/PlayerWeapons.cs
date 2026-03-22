using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public KnifeWeapon knifeWeapon;
    public MagicWeapon magicWeapon;
    public AuraWeapon auraWeapon;

    private void Start()
    {
        if (knifeWeapon != null)
            knifeWeapon.enabled = true;

        if (magicWeapon != null)
            magicWeapon.enabled = false;

        if (auraWeapon != null)
            auraWeapon.enabled = false;
    }

    public void UnlockMagic()
    {
        if (magicWeapon == null)
        {
            Debug.LogError("MagicWeapon не назначен");
            return;
        }

        magicWeapon.enabled = true;
        Debug.Log("Магия включена");
    }

    public void UnlockAura()
    {
        if (auraWeapon == null)
        {
            Debug.LogError("AuraWeapon не назначен");
            return;
        }

        auraWeapon.enabled = true;
        Debug.Log("Аура включена");
    }

    public void UpgradeKnifeDamage(int amount)
    {
        if (knifeWeapon != null)
        {
            knifeWeapon.damage += amount;
            Debug.Log("Урон ножа увеличен");
        }
    }

    public void UpgradeMagicDamage(int amount)
    {
        if (magicWeapon != null && magicWeapon.enabled)
        {
            magicWeapon.damage += amount;
            Debug.Log("Урон магии увеличен");
        }
    }

    public void UpgradeAuraDamage(int amount)
    {
        if (auraWeapon != null && auraWeapon.enabled)
        {
            auraWeapon.damage += amount;
            Debug.Log("Урон ауры увеличен");
        }
    }

    public bool HasMagic()
    {
        return magicWeapon != null && magicWeapon.enabled;
    }

    public bool HasAura()
    {
        return auraWeapon != null && auraWeapon.enabled;
    }
}