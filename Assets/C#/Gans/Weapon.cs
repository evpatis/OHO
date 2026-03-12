using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject пуляПрефаб;
    public float скоростьСтрельбы = 0.5f;
    public float скоростьПули = 10f;
    public float радиусАтаки = 5f;

    private float таймер;

    void Update()
    {
        

        таймер -= Time.deltaTime;

        if (таймер <= 0)
        {
            Transform цель = НайтиБлижайшегоВрага();
            if (цель != null)
            {
                Выстрелить(цель);
                таймер = скоростьСтрельбы;
            }
        }
    }

    Transform НайтиБлижайшегоВрага()
    {
        GameObject[] враги = GameObject.FindGameObjectsWithTag("Vrag");
        if (враги.Length == 0) return null;

        Transform ближайший = null;
        float минРасстояние = Mathf.Infinity;

        foreach (GameObject враг in враги)
        {
            if (враг == null) continue;
            if (!враг.activeInHierarchy) continue;

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
        GameObject пуля = Instantiate(пуляПрефаб, transform.position, Quaternion.identity);
        Vector2 направление = (цель.position - transform.position).normalized;
        пуля.GetComponent<Rigidbody2D>().linearVelocity = направление * скоростьПули;
    }
}