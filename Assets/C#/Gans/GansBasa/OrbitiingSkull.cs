using UnityEngine;

public class OrbitingSkull : MonoBehaviour
{
    public Transform player;
    public float radius = 2f;
    public float speed = 180f;
    public int damage = 1;

    private float angle;

   
    
    void Update()
    {
        if (player  == null) return;

        angle += speed * Time.deltaTime;

        float rad = angle * Mathf.Deg2Rad;

        Vector2 offset = new Vector2(Mathf.Cos (rad), Mathf.Sin(rad)) * radius;

        transform.position = (Vector2)player.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Череп коснулся: " + other.name);

        if (!other.CompareTag("Vrag")) return;

        Vrag enemy = other.GetComponent<Vrag>();
        if (enemy != null)
        {
            enemy.ПолучитьУрон(damage);
            Debug.Log("Череп нанес урон: " + damage);
        }
    }
}
