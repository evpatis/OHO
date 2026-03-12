using UnityEngine;

public class Vrag : MonoBehaviour
{
    public float скорость = 0.5f;
    public Transform цель;
    public Transform target;
    public Rigidbody2D физика;
    public int максимальноеЗдоровье = 3;
    private int текущееЗдоровье;


    void Start()
    {
        текущееЗдоровье = максимальноеЗдоровье;
        физика = GetComponent<Rigidbody2D>();
        GameObject игрок = GameObject.FindWithTag("Player");
        if (игрок != null)
            цель = игрок.transform;
    }
    public void ПолучитьУрон(int урон)
    {

        if (this == null) return;
        if (gameObject == null) return;

        текущееЗдоровье -= урон;
        Debug.Log("Урон! Осталось ХП: " + текущееЗдоровье);

        if (текущееЗдоровье <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (цель == null) return;

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
}