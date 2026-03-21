using UnityEngine;

public class LevelUpButtons : MonoBehaviour
{
    public ИгрокЗдоровье игрокЗдоровье;
    public PlayerWeapons playerWeapons;
    public ОпытИгрока опытИгрока;

    private void Start()
    {
        if (игрокЗдоровье == null)
            игрокЗдоровье = FindObjectOfType<ИгрокЗдоровье>();

        if (playerWeapons == null)
            playerWeapons = FindObjectOfType<PlayerWeapons>();

        if (опытИгрока == null)
            опытИгрока = FindObjectOfType<ОпытИгрока>();
    }

    public void AddHealth()
    {
        if (игрокЗдоровье != null)
        {
            игрокЗдоровье.максимальноеХП += 2;
            игрокЗдоровье.текущееХП += 2;
        }

        if (опытИгрока != null)
            опытИгрока.ЗакрытьВыборУлучшения();
    }

    public void UnlockMagic()
    {
        if (playerWeapons != null)
            playerWeapons.UnlockMagic();

        if (опытИгрока != null)
            опытИгрока.ЗакрытьВыборУлучшения();
    }

    public void UpgradeKnifeDamage()
    {
        if (playerWeapons != null)
            playerWeapons.UpgradeKnifeDamage(1);

        if (опытИгрока != null)
            опытИгрока.ЗакрытьВыборУлучшения();
    }
}