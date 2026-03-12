using UnityEngine;

public class SpawnVrag : MonoBehaviour
{
    public GameObject vragPrefab;
    public float interval = 2f;
    public float spawnRadius = 15f; 

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Spawn();
            timer = interval;
        }
    }

    void Spawn()
    {
        
        Vector2 pos = Random.insideUnitCircle * spawnRadius;

        Instantiate(vragPrefab, pos, Quaternion.identity);
    }
}