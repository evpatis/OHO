using TMPro;
using UnityEngine;

public class UpgradeCardUI : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    private UpgradeManager.UpgradeData currentData;
    private UpgradeManager upgradeManager;

    public void Setup(UpgradeManager.UpgradeData data, UpgradeManager manager)
    {
        currentData = data;
        upgradeManager = manager;

        if (titleText != null)
            titleText.text = data.title;

        if (descriptionText != null)
            descriptionText.text = data.description;
    }

    public void OnClickCard()
    {
        if (upgradeManager != null)
        {
            upgradeManager.ApplyUpgrade(currentData);
        }
    }
}