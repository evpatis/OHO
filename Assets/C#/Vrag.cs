using UnityEngine;

public class Vrag : MonoBehaviour
{
    public float скорость = 0.5f;
    public Transform цель;
    public Transform target;
    public Rigidbody2D физика;


    void Start()
    {
        физика = GetComponent<Rigidbody2D>();
        GameObject игрок = GameObject.FindWithTag("Player");
        if (игрок != null)
            цель = игрок.transform;
    }


    void FixedUpdate()
    {
        if (цель == null) return;

        Vector2 направление = (цель.position - transform.position).normalized;
        физика.linearVelocity = направление * скорость;

        // Отталкивание от других врагов
        Collider2D[] соседи = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D сосед in соседи)
        {
            if (сосед.gameObject != gameObject && сосед.CompareTag("vrag"))
            {
                Vector2 оттолкнуть = (transform.position - сосед.transform.position).normalized;
                физика.AddForce(оттолкнуть * 10f);
            }
        }
    }
}