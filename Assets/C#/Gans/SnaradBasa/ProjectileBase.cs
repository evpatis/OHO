//БАЗА снаряда сюда не лезем, все остальное оружие наследуется от этого класса 
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float скорость = 10f;
    public int урон = 1;
    public float времяЖизни = 3f;

    protected Vector2 направление;

    public void Инициализация(Vector2 dir)
    {
        направление = dir.normalized;
    }

    void Start()
    {
        Destroy(gameObject, времяЖизни);
    }

    void Update()
    {
        transform.position += (Vector3)(направление * скорость * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D другой)
    {
        if (!другой.CompareTag("Vrag")) return;

        Vrag враг = другой.GetComponent<Vrag>();

        if (враг != null)
            враг.ПолучитьУрон(урон);

        Destroy(gameObject);
    }
}