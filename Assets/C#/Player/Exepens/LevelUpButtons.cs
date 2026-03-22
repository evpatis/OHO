using UnityEngine;
using UnityEngine.UI;

public class LevelUpButtons : MonoBehaviour
{
    public ИгрокЗдоровье игрокЗдоровье;
    public PlayerWeapons playerWeapons;
    public ОпытИгрока опытИгрока;

    [Header("Панели")]
    public GameObject upgradePanel;

    [Header("Кнопки апгрейда")]
    public GameObject buttonKnifeUpgrade;
    public GameObject buttonMagicUpgrade;
    public GameObject buttonAuraUpgrade;

    private void Start()
    {
        if (игрокЗдоровье == null)
            игрокЗдоровье = FindObjectOfType<ИгрокЗдоровье>();

        if (playerWeapons == null)
            playerWeapons = FindObjectOfType<PlayerWeapons>();

        if (опытИгрока == null)
            опытИгрока = FindObjectOfType<ОпытИгрока>();

        if (upgradePanel != null)
            upgradePanel.SetActive(false);
    }

    public void AddHealth()
    {
        if (игрокЗдоровье != null)
        {
            игрокЗдоровье.максимальноеХП += 2;
            игрокЗдоровье.текущееХП += 2;
            Debug.Log("Игрок получил +2 HP");
        }

        ЗакрытьВсеПанели();
    }

    public void UnlockNextWeapon()
    {
        if (playerWeapons == null)
        {
            ЗакрытьВсеПанели();
            return;
        }

        if (!playerWeapons.HasMagic())
        {
            playerWeapons.UnlockMagic();
            Debug.Log("Открыто новое оружие: Магия");
        }
        else if (!playerWeapons.HasAura())
        {
            playerWeapons.UnlockAura();
            Debug.Log("Открыто новое оружие: Аура");
        }
        else
        {
            Debug.Log("Все текущие оружия уже открыты");
        }

        ЗакрытьВсеПанели();
    }

    public void OpenUpgradeMenu()
    {
        if (upgradePanel == null) return;

        if (buttonKnifeUpgrade != null)
            buttonKnifeUpgrade.SetActive(true);

        if (buttonMagicUpgrade != null)
            buttonMagicUpgrade.SetActive(playerWeapons != null && playerWeapons.HasMagic());

        if (buttonAuraUpgrade != null)
            buttonAuraUpgrade.SetActive(playerWeapons != null && playerWeapons.HasAura());

        upgradePanel.SetActive(true);
    }

    public void UpgradeKnife()
    {
        if (playerWeapons != null)
        {
            playerWeapons.UpgradeKnifeDamage(1);
        }

        ЗакрытьВсеПанели();
    }

    public void UpgradeMagic()
    {
        if (playerWeapons != null)
        {
            playerWeapons.UpgradeMagicDamage(1);
        }

        ЗакрытьВсеПанели();
    }

    public void UpgradeAura()
    {
        if (playerWeapons != null)
        {
            playerWeapons.UpgradeAuraDamage(1);
        }

        ЗакрытьВсеПанели();
    }

    private void ЗакрытьВсеПанели()
    {
        if (upgradePanel != null)
            upgradePanel.SetActive(false);

        if (опытИгрока != null)
            опытИгрока.ЗакрытьВыборУлучшения();
    }
}