
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public KnifeWeapon KnifeWeapon;
    public MagicWeapon magicWeapon;

    private void Start()
    {
        if (KnifeWeapon != null)
            KnifeWeapon.enabled = true;

        if (magicWeapon != null)
            magicWeapon.enabled = false;
    }

    public void UnlockMagic()
    { 
        if (magicWeapon == null);
        {
            Debug.LogError("MagicWeapon не назначен PlayerWeapons");
            return;
        }

        if (!magicWeapon.enabled)
        {
            magicWeapon.enabled = true;
            Debug.Log("Магия открыта");
        }

        else
        {
            Debug.Log("Магия уже была открыта");
        }
    }

    public void UpgradeKnifeDamage (int amount)
    {
        if (KnifeWeapon != null)
        {
            KnifeWeapon.damage +=  amount;
            Debug.Log("Урон ножа увеличен");
        }
    }

    public void UpgradeKnifeCooldow(float amount)
    { 
        if (KnifeWeapon != null)
        {
            KnifeWeapon.cooldown = Mathf.Max(0.2f, KnifeWeapon.cooldown - amount );
            Debug.Log("Скорость ножа увеличен");
        }
    }

    public void UpgradeMagicDamage (int amount)
    { 
    if (magicWeapon !=null)
        {
            magicWeapon.damage += amount;
            Debug.Log("Урон магии увеличен");
        }
    }

}