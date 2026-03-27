using UnityEngine;

public class SkeletonSpawnEffect : MonoBehaviour
{
    public float riseTime = 0.5f;
    public float startYOffset = -1f;

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        targetPosition = transform.position;
        startPosition = targetPosition + new Vector3(0f, startYOffset, 0f);
        transform.position = startPosition;
    }

    void Update()
    {
        if (timer < riseTime)
        {
            timer += Time.deltaTime;
            float t = timer / riseTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
        }
    }
}