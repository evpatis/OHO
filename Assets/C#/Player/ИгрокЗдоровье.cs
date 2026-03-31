using UnityEngine;

public class ИгрокЗдоровье : MonoBehaviour
{
    public GameObject gameOverPanel;

    private bool мертв = false;

    private PlayerStats stats;

    private void Start()
    {
        Time.timeScale = 1f;

        stats = GetComponent<PlayerStats>();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void ПолучитьУрон(int урон)
    {
        if (мертв) return;
        if (stats == null) return;

        int остатокУрона = урон;

        
        if (stats.currentArmor > 0)
        {
            if (stats.currentArmor >= остатокУрона)
            {
                stats.currentArmor -= остатокУрона;
                остатокУрона = 0;
            }
            else
            {
                остатокУрона -= stats.currentArmor;
                stats.currentArmor = 0;
            }
        }

        
        if (остатокУрона > 0)
        {
            stats.currentHP -= остатокУрона;
        }

        Debug.Log("HP: " + stats.currentHP + " | Armor: " + stats.currentArmor);

        if (stats.currentHP <= 0)
        {
            Смерть();
        }
    }

    void Смерть()
    {
        if (мертв) return;
        мертв = true;

        Debug.Log("Игрок умер");

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}