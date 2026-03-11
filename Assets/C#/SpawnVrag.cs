using UnityEngine;

public class SpawnVrag : MonoBehaviour
{
    public GameObject vragПрефаб;
    public int максимальноеКоличествоВрагов = 10;
    public float интервалСпавна = 2f;
    public Vector2 границыСпавна = new Vector2(20f, 10f); // ширина и высота области спавна

    private float таймер;

    void Start()
    {
        таймер = интервалСпавна;
    }

    void Update()
    {
        таймер -= Time.deltaTime;

        if (таймер <= 0)
        {
            // Проверяем, не слишком ли много врагов
            GameObject[] vrag = GameObject.FindGameObjectsWithTag("vrag");
            if (vrag.Length < максимальноеКоличествоВрагов)
            {
                Спавнить();
            }
            таймер = интервалСпавна;
        }
    }

    void Спавнить()
    {
        // Рандомная позиция в пределах границ
        float x = Random.Range(-границыСпавна.x / 2, границыСпавна.x / 2);
        float y = Random.Range(-границыСпавна.y / 2, границыСпавна.y / 2);
        Vector2 позиция = new Vector2(x, y);

        Instantiate(vragПрефаб, позиция, Quaternion.identity);
    }

    // Для отладки — визуализация области спавна
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, границыСпавна);
    }
}