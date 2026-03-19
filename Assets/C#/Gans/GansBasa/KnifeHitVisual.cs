using UnityEngine;

public class KnifeHitVisual : MonoBehaviour
{
    public float lifeTime = 0.1f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
