using UnityEngine;

public class ОпытИгрока : MonoBehaviour
{
    public int уровень = 1;
    public int текущийОпыт = 0;
    public int опытДоСледующегоУровня = 20;

    public GameObject panelLevelUp;
    public UpgradeManager upgradeManager;

    private bool выборУлучшенияОткрыт = false;

    private void Start()
    {
        if (panelLevelUp != null)
            panelLevelUp.SetActive(false);
    }

    public void ДобавитьОпыт(int количество)
    {
        if (выборУлучшенияОткрыт) return;

        текущийОпыт += количество;

        Debug.Log("Игрок получил опыт: " + количество);
        Debug.Log("Опыт: " + текущийОпыт + " / " + опытДоСледующегоУровня);

        while (текущийОпыт >= опытДоСледующегоУровня)
        {
            ПовыситьУровень();
        }
    }

    void ПовыситьУровень()
    {
        текущийОпыт -= опытДоСледующегоУровня;
        уровень++;

        опытДоСледующегоУровня += 10;

        Debug.Log("Уровень повышен! Новый уровень: " + уровень);

        ОткрытьВыборУлучшения();
    }

    void ОткрытьВыборУлучшения()
    {
        выборУлучшенияОткрыт = true;
        Time.timeScale = 0f;

        if (panelLevelUp != null)
            panelLevelUp.SetActive(true);

        if (upgradeManager != null)
            upgradeManager.ShowRandomUpgrades();
    }

    public void ЗакрытьВыборУлучшения()
    {
        выборУлучшенияОткрыт = false;
        Time.timeScale = 1f;

        if (panelLevelUp != null)
            panelLevelUp.SetActive(false);
    }
}