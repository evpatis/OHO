using UnityEngine;

public class ИгрокЗдоровье : MonoBehaviour
{
    public int максимальноеЗдоровье = 10;
    public int текущееЗдоровье;

    private void Start()
    {
        текущееЗдоровье = максимальноеЗдоровье;
    }

    public void ПолучитьУрон(int урон)
    {
        текущееЗдоровье -= урон;
        Debug.Log("Игрок получил урон. ХП: " + текущееЗдоровье);

        if (текущееЗдоровье <= 0)
        {
            Смерть();
        }
    }

    void Смерть()
    {
        Debug.Log("Игрок умер");
        gameObject.SetActive(false);
    }
}
