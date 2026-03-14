/*
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
        if (vragPrefab == null)
        {
            Debug.LogError("vragPrefab не назначен!");
            return;
        }

        Vector2 pos = Random.insideUnitCircle * spawnRadius;
        Instantiate(vragPrefab, pos, Quaternion.identity);
    }
}
*/
/*
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawn Settings")]
    public float minSpawnInterval = 0.5f;
    public float maxSpawnInterval = 1.2f;

    public float minSpawnDistance = 10f;
    public float maxSpawnDistance = 30f;

    public float minSafeDistanceFromCamera = 6f; // защита от респа перед персом 

    [Header("References")]
    public Camera mainCamera;

    private float timer;

    private Vector3 lastCameraPosition;
    private Vector2 cameraMoveDirection = Vector2.right;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (enemyPrefab == null)
            Debug.LogError("Enemy prefab not assigned!");

        if (mainCamera != null)
            lastCameraPosition = mainCamera.transform.position;

        timer = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        UpdateCameraMovement();

        if (timer <= 0)
        {
            SpawnEnemy();
            timer = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void UpdateCameraMovement()
    {
        if (mainCamera == null)
            return;

        Vector3 delta = mainCamera.transform.position - lastCameraPosition;

        if (delta.sqrMagnitude > 0.001f)
        {
            cameraMoveDirection = delta.normalized;
        }

        lastCameraPosition = mainCamera.transform.position;
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || mainCamera == null)
            return;

        Vector2 spawnPos = Vector2.zero;

        int attempts = 0;
        const int maxAttempts = 40;

        bool foundPosition = false;

        while (!foundPosition && attempts < maxAttempts)
        {
            Vector2 direction;

            if (cameraMoveDirection.sqrMagnitude > 0.1f)
            {
                // 50% впереди, 50% сзади
                direction = Random.value > 0.5f ? cameraMoveDirection : -cameraMoveDirection;
            }
            else
            {
                direction = Random.insideUnitCircle.normalized;
            }

            //  случайный разброс
            direction = (direction + Random.insideUnitCircle * 0.6f).normalized;

            float distance = Random.Range(minSpawnDistance, maxSpawnDistance);

            spawnPos = (Vector2)mainCamera.transform.position + direction * distance;

            Vector3 viewport = mainCamera.WorldToViewportPoint(spawnPos);

            bool visible =
                viewport.x > -0.15f && viewport.x < 1.15f &&
                viewport.y > -0.15f && viewport.y < 1.15f &&
                viewport.z > 0;

            float distToCamera = Vector2.Distance(spawnPos, mainCamera.transform.position);

            if (!visible && distToCamera > minSafeDistanceFromCamera)
            {
                foundPosition = true;
            }

            attempts++;
        }

        if (!foundPosition)
        {
            Vector2 fallback = Random.insideUnitCircle.normalized * minSpawnDistance;
            spawnPos = (Vector2)mainCamera.transform.position + fallback;
        }

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
*/
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawn Settings")]
    public float minSpawnInterval = 0.4f;
    public float maxSpawnInterval = 1.0f;

    public float spawnOffset = 2f; // насколько далеко за экраном спавн

    [Header("References")]
    public Camera mainCamera;

    float timer;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        timer = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnEnemy();
            timer = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || mainCamera == null)
            return;

        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        Vector3 camPos = mainCamera.transform.position;

        Vector2 spawnPos = Vector2.zero;

        int side = Random.Range(0, 4);

        switch (side)
        {
            // LEFT
            case 0:
                spawnPos = new Vector2(
                    camPos.x - camWidth - spawnOffset,
                    Random.Range(camPos.y - camHeight, camPos.y + camHeight)
                );
                break;

            // RIGHT
            case 1:
                spawnPos = new Vector2(
                    camPos.x + camWidth + spawnOffset,
                    Random.Range(camPos.y - camHeight, camPos.y + camHeight)
                );
                break;

            // TOP
            case 2:
                spawnPos = new Vector2(
                    Random.Range(camPos.x - camWidth, camPos.x + camWidth),
                    camPos.y + camHeight + spawnOffset
                );
                break;

            // BOTTOM
            case 3:
                spawnPos = new Vector2(
                    Random.Range(camPos.x - camWidth, camPos.x + camWidth),
                    camPos.y - camHeight - spawnOffset
                );
                break;
        }

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}