using UnityEngine;

public class Vrag : MonoBehaviour
{
    public float скорость = 0.5f;
    public Transform цель;
    public Rigidbody2D физика;

    public int максимальноеЗдоровье = 3;
    public int текущееЗдоровье;

    public GameObject душаПрефаб;


    void Start()
    {
        текущееЗдоровье = максимальноеЗдоровье;
        физика = GetComponent<Rigidbody2D>();
        GameObject игрок = GameObject.FindWithTag("Player");
        if (игрок != null)
            цель = игрок.transform;
    }
    /*
    public void ПолучитьУрон(int урон)
    {

        if (this == null) return;
        if (gameObject == null) return;

        текущееЗдоровье -= урон;
        Debug.Log("Урон! Осталось ХП: " + текущееЗдоровье);

        if (текущееЗдоровье <= 0)
        {
            Смерть();
        }
    }
    */
    public void ПолучитьУрон(int урон)
    {
        текущееЗдоровье -= урон;
        Debug.Log("Урон! Текущее ХП: " + текущееЗдоровье);

        if (текущееЗдоровье <= 0)
        {
            Debug.Log("Враг умирает!");
            Destroy(gameObject); // прямо здесь, без отдельного метода
        }
    }

    void Смерть()
    {
        Debug.Log("Враг умер: " + gameObject.name);

        if (душаПрефаб != null)
        {
            Instantiate(душаПрефаб, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        // Проверка что объект существует и активен
        if (gameObject == null || !gameObject.activeInHierarchy) return;

        // Проверка цели
        if (цель == null) return;
        if (цель.gameObject == null) return;
        if (!цель.gameObject.activeInHierarchy) return;

        Vector2 направление = (цель.position - transform.position).normalized;
        физика.linearVelocity = направление * скорость;

        Collider2D[] соседи = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D сосед in соседи)
        {
            if (сосед.gameObject != gameObject && сосед.CompareTag("Vrag"))
            {
                Vector2 оттолкнуть = (transform.position - сосед.transform.position).normalized;
                физика.AddForce(оттолкнуть * 10f);
            }
        }
    }
    void OnDestroy()
    {
        Debug.Log("OnDestroy вызван для: " + gameObject.name);
        // Очищаем ссылку на цель, если это игрок
        цель = null;
    }

}