using UnityEngine;

public class HitVisual : MonoBehaviour
{
    public float lifetime = 0.15f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
