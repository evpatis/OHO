using UnityEngine;

public class bullet : MonoBehaviour
{
    public int урон = 1;
    public float времяЖизни = 2f;

    void Start()
    {
        Destroy(gameObject, времяЖизни);
    }

    void OnTriggerEnter2D(Collider2D другой)
    {
        if (другой.CompareTag("Vrag"))
        {
            Vrag враг = другой.GetComponent<Vrag>();
            if (враг != null)
            {
                враг.ПолучитьУрон(урон);
            }
            Destroy(gameObject);
        }
    }
}