using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public KnifeWeapon knifeWeapon;
    public MagicWeapon magicWeapon;
    public AuraWeapon auraWeapon;
    public SkullWeapon skullWeapon;
    public NecroBombWeapon necroBombWeapon;

    private void Start()
    {
        if (knifeWeapon != null)
            knifeWeapon.enabled = true;

        if (magicWeapon != null)
            magicWeapon.enabled = false;

        if (auraWeapon != null)
            auraWeapon.enabled = false;

        if (skullWeapon != null)
            skullWeapon.enabled = false;

        if (necroBombWeapon != null)
            necroBombWeapon.enabled = false;
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

    public void UnlockSkull()
    {
        if (skullWeapon == null)
        {
            Debug.LogError("SkullWeapon не назначен");
            return;
        }

        skullWeapon.enabled = true;
        Debug.Log("Череп включен");
    }

    public void UnlockNecroBomb()
    {
        if (necroBombWeapon == null)
        {
            Debug.LogError("NecroBombWeapon не назначен");
            return;
        }

        necroBombWeapon.enabled = true;
        Debug.Log("Некро-бомба включена");
    }

    public void UpgradeKnifeDamage(int amount)
    {
        if (knifeWeapon != null)
        {
            knifeWeapon.damage += amount;
            Debug.Log("Урон ножа увеличен");
        }
    }

    public void UpgradeKnifeCooldown(float amount)
    {
        if (knifeWeapon != null)
        {
            knifeWeapon.cooldown = Mathf.Max(0.2f, knifeWeapon.cooldown - amount);
            Debug.Log("Скорость ножа увеличена");
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

    public void UpgradeAuraRadius(float amount)
    {
        if (auraWeapon != null && auraWeapon.enabled)
        {
            auraWeapon.radius += amount;
            Debug.Log("Радиус ауры увеличен");
        }
    }

    public void UpgradeSkullDamage(int amount)
    {
        if (skullWeapon != null && skullWeapon.enabled)
        {
            skullWeapon.damage += amount;
            Debug.Log("Урон черепа увеличен");
        }
    }

    public void UpgradeNecroBombDamage(int amount)
    {
        if (necroBombWeapon != null && necroBombWeapon.enabled)
        {
            necroBombWeapon.explodeDamage += amount;
            Debug.Log("Урон некро-бомбы увеличен");
        }
    }

    public bool HasMagic() => magicWeapon != null && magicWeapon.enabled;
    public bool HasAura() => auraWeapon != null && auraWeapon.enabled;
    public bool HasSkull() => skullWeapon != null && skullWeapon.enabled;
    public bool HasNecroBomb() => necroBombWeapon != null && necroBombWeapon.enabled;
}