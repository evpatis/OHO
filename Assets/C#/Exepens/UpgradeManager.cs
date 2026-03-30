using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public ОпытИгрока опытИгрока;
    public ИгрокЗдоровье игрокЗдоровье;
    public PlayerWeapons playerWeapons;

    public UpgradeCardUI card1;
    public UpgradeCardUI card2;
    public UpgradeCardUI card3;

    public enum UpgradeType
    {
        HealthUp,
        MoveSpeedUp,

        OpenMagic,
        OpenAura,
        OpenSkull,
        OpenNecroBomb,

        KnifeDamage,
        KnifeSpeed,

        MagicDamage,

        AuraDamage,
        AuraRadius,

        SkullDamage,

        NecroBombDamage
    }

    [System.Serializable]
    public class UpgradeData
    {
        public UpgradeType type;
        public string title;
        public string description;
    }

    public void ShowRandomUpgrades()
    {
        List<UpgradeData> possibleUpgrades = GetAvailableUpgrades();

        if (possibleUpgrades.Count == 0)
        {
            Debug.Log("Нет доступных улучшений");
            return;
        }

        List<UpgradeData> selected = new List<UpgradeData>();
        List<UpgradeData> pool = new List<UpgradeData>(possibleUpgrades);

        for (int i = 0; i < 3 && pool.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, pool.Count);
            selected.Add(pool[randomIndex]);
            pool.RemoveAt(randomIndex);
        }

        card1.gameObject.SetActive(selected.Count > 0);
        card2.gameObject.SetActive(selected.Count > 1);
        card3.gameObject.SetActive(selected.Count > 2);

        if (selected.Count > 0) card1.Setup(selected[0], this);
        if (selected.Count > 1) card2.Setup(selected[1], this);
        if (selected.Count > 2) card3.Setup(selected[2], this);
    }

    List<UpgradeData> GetAvailableUpgrades()
    {
        List<UpgradeData> upgrades = new List<UpgradeData>();

        upgrades.Add(new UpgradeData
        {
            type = UpgradeType.HealthUp,
            title = "+2 HP",
            description = "Увеличить здоровье игрока"
        });

        upgrades.Add(new UpgradeData
        {
            type = UpgradeType.MoveSpeedUp,
            title = "+10% скорость",
            description = "Персонаж двигается быстрее"
        });

        if (!playerWeapons.HasMagic())
        {
            upgrades.Add(new UpgradeData
            {
                type = UpgradeType.OpenMagic,
                title = "Новая магия",
                description = "Открыть магическое оружие"
            });
        }

        if (!playerWeapons.HasAura())
        {
            upgrades.Add(new UpgradeData
            {
                type = UpgradeType.OpenAura,
                title = "Новая аура",
                description = "Открыть ауру вокруг героя"
            });
        }

        if (!playerWeapons.HasSkull())
        {
            upgrades.Add(new UpgradeData
            {
                type = UpgradeType.OpenSkull,
                title = "Новый череп",
                description = "Открыть орбитальный череп"
            });
        }

        if (!playerWeapons.HasNecroBomb())
        {
            upgrades.Add(new UpgradeData
            {
                type = UpgradeType.OpenNecroBomb,
                title = "Некро-бомба",
                description = "Открыть союзного скелета-бомбу"
            });
        }

        upgrades.Add(new UpgradeData
        {
            type = UpgradeType.KnifeDamage,
            title = "Нож + урон",
            description = "Увеличить урон ножа"
        });

        upgrades.Add(new UpgradeData
        {
            type = UpgradeType.KnifeSpeed,
            title = "Нож + скорость",
            description = "Нож бьёт быстрее"
        });

        if (playerWeapons.HasMagic())
        {
            upgrades.Add(new UpgradeData
            {
                type = UpgradeType.MagicDamage,
                title = "Магия + урон",
                description = "Увеличить урон магии"
            });
        }

        if (playerWeapons.HasAura())
        {
            upgrades.Add(new UpgradeData
            {
                type = UpgradeType.AuraDamage,
                title = "Аура + урон",
                description = "Увеличить урон ауры"
            });

            upgrades.Add(new UpgradeData
            {
                type = UpgradeType.AuraRadius,
                title = "Аура + радиус",
                description = "Увеличить радиус ауры"
            });
        }

        if (playerWeapons.HasSkull())
        {
            upgrades.Add(new UpgradeData
            {
                type = UpgradeType.SkullDamage,
                title = "Череп + урон",
                description = "Увеличить урон черепа"
            });
        }

        if (playerWeapons.HasNecroBomb())
        {
            upgrades.Add(new UpgradeData
            {
                type = UpgradeType.NecroBombDamage,
                title = "Бомба + урон",
                description = "Увеличить урон некро-бомбы"
            });
        }

        return upgrades;
    }

    public void ApplyUpgrade(UpgradeData data)
    {
        switch (data.type)
        {
            case UpgradeType.HealthUp:
                игрокЗдоровье.максимальноеХП += 2;
                игрокЗдоровье.текущееХП += 2;
                break;

            case UpgradeType.MoveSpeedUp:
                Движение movement = FindObjectOfType<Движение>();
                if (movement != null)
                    movement.скорость *= 1.1f;
                break;

            case UpgradeType.OpenMagic:
                playerWeapons.UnlockMagic();
                break;

            case UpgradeType.OpenAura:
                playerWeapons.UnlockAura();
                break;

            case UpgradeType.OpenSkull:
                playerWeapons.UnlockSkull();
                break;

            case UpgradeType.OpenNecroBomb:
                playerWeapons.UnlockNecroBomb();
                break;

            case UpgradeType.KnifeDamage:
                playerWeapons.UpgradeKnifeDamage(1);
                break;

            case UpgradeType.KnifeSpeed:
                playerWeapons.UpgradeKnifeCooldown(0.15f);
                break;

            case UpgradeType.MagicDamage:
                playerWeapons.UpgradeMagicDamage(1);
                break;

            case UpgradeType.AuraDamage:
                playerWeapons.UpgradeAuraDamage(1);
                break;

            case UpgradeType.AuraRadius:
                playerWeapons.UpgradeAuraRadius(0.3f);
                break;

            case UpgradeType.SkullDamage:
                playerWeapons.UpgradeSkullDamage(1);
                break;

            case UpgradeType.NecroBombDamage:
                playerWeapons.UpgradeNecroBombDamage(2);
                break;
        }

        опытИгрока.ЗакрытьВыборУлучшения();
    }
}