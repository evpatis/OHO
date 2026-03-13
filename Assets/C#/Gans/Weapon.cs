using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject пуляПрефаб;
    public float скоростьСтрельбы = 0.5f;
    public float скоростьПули = 10f;
    public float радиусАтаки = 5f;

    private float таймер;
    private List<GameObject> живыеВраги = new List<GameObject>(); // Кэш врагов

    void Update()
    {
        // Обновляем список живых врагов
        ОбновитьСписокВрагов();

        if (живыеВраги.Count == 0) return;

        таймер -= Time.deltaTime;

        if (таймер <= 0)
        {
            Transform цель = НайтиБлижайшегоВрага();
            if (цель != null)
            {
                Выстрелить(цель);
            }
            таймер = скоростьСтрельбы;
        }
    }

    void ОбновитьСписокВрагов()
    {
        // Очищаем старый список
        живыеВраги.Clear();

        // Находим всех врагов
        GameObject[] всеВраги = GameObject.FindGameObjectsWithTag("Vrag");

        // Добавляем только живых
        foreach (GameObject враг in всеВраги)
        {
            if (враг != null && враг.activeInHierarchy)
            {
                Vrag компонентВрага = враг.GetComponent<Vrag>();
                if (компонентВрага != null && компонентВрага.текущееЗдоровье > 0)
                {
                    живыеВраги.Add(враг);
                }
            }
        }
    }

    Transform НайтиБлижайшегоВрага()
    {
        if (живыеВраги.Count == 0) return null;

        Transform ближайший = null;
        float минРасстояние = Mathf.Infinity;

        // Проходим по всем живым врагам
        for (int i = живыеВраги.Count - 1; i >= 0; i--)
        {
            GameObject враг = живыеВраги[i];

            // Проверяем, существует ли враг и жив ли он
            if (враг == null || !враг.activeInHierarchy)
            {
                живыеВраги.RemoveAt(i);
                continue;
            }

            Vrag компонентВрага = враг.GetComponent<Vrag>();
            if (компонентВрага == null || компонентВрага.текущееЗдоровье <= 0)
            {
                живыеВраги.RemoveAt(i);
                continue;
            }

            float расстояние = Vector2.Distance(transform.position, враг.transform.position);
            if (расстояние < минРасстояние && расстояние <= радиусАтаки)
            {
                минРасстояние = расстояние;
                ближайший = враг.transform;
            }
        }

        return ближайший;
    }

    void Выстрелить(Transform цель)
    {
        if (цель == null) return;

        // Дополнительная проверка через try-catch на случай уничтожения цели
        try
        {
            GameObject пуля = Instantiate(пуляПрефаб, transform.position, Quaternion.identity);

            // Проверяем, существует ли цель
            if (цель != null && цель.gameObject != null)
            {
                Vector2 направление = (цель.position - transform.position).normalized;
                Rigidbody2D rb = пуля.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = направление * скоростьПули;
                }
            }
        }
        catch (MissingReferenceException)
        {
            // Если цель уничтожена - просто пропускаем выстрел
            Debug.Log("Цель была уничтожена");
        }
    }

    // Для отладки - визуализация радиуса атаки
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, радиусАтаки);
    }
}