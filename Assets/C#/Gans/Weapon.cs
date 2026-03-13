using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject пуляПрефаб;
    public float скоростьСтрельбы = 0.5f;
    public float скоростьПули = 10f;
    public float радиусАтаки = 1f;

    private float таймер;
    private List<GameObject> живыеВраги = new List<GameObject>();
    
    void Update()
    {
        ОбновитьСписокВрагов();
        Debug.Log("Живых врагов в списке: " + живыеВраги.Count); // Эту строку

        //if (живыеВраги.Count == 0) return;//
       

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

    /*
    void ОбновитьСписокВрагов()
    {
        
        живыеВраги.Clear();

        
        GameObject[] всеВраги = GameObject.FindGameObjectsWithTag("Vrag");

        
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
    */
    void ОбновитьСписокВрагов()
    {
        живыеВраги.Clear();
        GameObject[] всеВраги = GameObject.FindGameObjectsWithTag("Vrag");

        foreach (GameObject враг in всеВраги)
        {
            if (враг != null && враг.activeInHierarchy)
            {
                // Добавляем ВСЕХ активных врагов, даже с ХП = 1
                живыеВраги.Add(враг);
                Debug.Log("Добавлен враг: " + враг.name);
            }
        }

        Debug.Log("Всего врагов: " + живыеВраги.Count);
    }

    /*
    Transform НайтиБлижайшегоВрага()
    {
        if (живыеВраги.Count == 0) return null;

        

        Transform ближайший = null;
        float минРасстояние = Mathf.Infinity;

        
        for (int i = живыеВраги.Count - 1; i >= 0; i--)
        {
            GameObject враг = живыеВраги[i];

            
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

    */
    Transform НайтиБлижайшегоВрага()
    {
        if (живыеВраги.Count == 0) return null;

        // Очистка списка от уничтоженных объектов
        for (int i = живыеВраги.Count - 1; i >= 0; i--)
        {
            if (живыеВраги[i] == null)
            {
                живыеВраги.RemoveAt(i);
            }
        }

        Transform ближайший = null;
        float минРасстояние = Mathf.Infinity;

        foreach (GameObject враг in живыеВраги)
        {
            if (враг == null) continue;

            float расстояние = Vector2.Distance(transform.position, враг.transform.position);

            if (расстояние < минРасстояние)
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
        if (пуляПрефаб == null) return;

        // Проверка что цель не уничтожена
        if (цель.gameObject == null)
        {
            Debug.Log("Цель.gameObject = null");
            return;
        }

        GameObject пуля = Instantiate(пуляПрефаб, transform.position, Quaternion.identity);

        try
        {
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
            Debug.Log("Цель уничтожена в момент выстрела");
        }
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, радиусАтаки);
    }
}