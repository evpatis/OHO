using UnityEngine;

public class ИгрокЗдоровье : MonoBehaviour
{
    public int максимальноеХП = 10;
    public int текущееХП;

    public GameObject gameOverPanel;

    private bool мертв = false;

    private void Start()
    {
        Time.timeScale = 1f;
        текущееХП = максимальноеХП;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void ПолучитьУрон(int урон)
    {
        if (мертв) return;

        текущееХП -= урон;
        Debug.Log("Игрок получил урон. ХП: " + текущееХП);

        if (текущееХП <= 0)
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
            Debug.Log("Панель найдена");
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("gameOverPanel не назначен!");
        }

        Time.timeScale = 0f;
    }
}
