using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ТипВрага 
{
    public GameObject префаб;
    public float времяПоявления;
         
}

public class SpawnVrag : MonoBehaviour
{
    [Header("Типы врагов")]
    public List<ТипВрага> враги = new List<ТипВрага>();

    [Header("Настройки спавна")]
    public float minSpawnInterval = 0.4f;
    public float maxSpawnInterval = 1.0f;
    public float spawnOffset = 2f;

    [Header("Ссылки")]
    public Camera mainCamera;

    public float timer;
    private float времяИгры;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        timer = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        времяИгры += Time.deltaTime;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnEnemy();
            timer = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnEnemy()
    {
        if (mainCamera == null || враги.Count == 0)
            return;

        List<GameObject> доступныеВраги = new List<GameObject>();

        foreach(ТипВрага враг in враги)
        {
            if (враг.префаб != null && времяИгры >= враг.времяПоявления)
            {
                доступныеВраги.Add(враг.префаб);
            }
        }

        if (доступныеВраги.Count == 0)
            return;

        GameObject выбранныйВраг = доступныеВраги[Random.Range(0, доступныеВраги.Count)];

        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;
        Vector3 camPos = mainCamera.transform.position;

        Vector2 spawnPos = Vector2.zero;
        int side = Random.Range(0, 4);

        switch (side)
        { 
        case 0: // лево
            spawnPos = new Vector2(
                camPos.x - camWidth - spawnOffset,
                Random.Range(camPos.y - camHeight, camPos.y + camHeight)
            );
            break;

        case 1: // право
            spawnPos = new Vector2(
                camPos.x + camWidth + spawnOffset,
                Random.Range(camPos.y - camHeight, camPos.y + camHeight)
            );
            break;

        case 2: // верх
            spawnPos = new Vector2(
                Random.Range(camPos.x - camWidth, camPos.x + camWidth),
                camPos.y + camHeight + spawnOffset
            );
            break;

        case 3: // низ
            spawnPos = new Vector2(
                Random.Range(camPos.x - camWidth, camPos.x + camWidth),
                camPos.y - camHeight - spawnOffset
            );
            break;
        }

        Instantiate(выбранныйВраг, spawnPos, Quaternion.identity);
    }
}

/*using UnityEngine;

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
            // лево 
            case 0:
                spawnPos = new Vector2(
                    camPos.x - camWidth - spawnOffset,
                    Random.Range(camPos.y - camHeight, camPos.y + camHeight)
                );
                break;

            // право
            case 1:
                spawnPos = new Vector2(
                    camPos.x + camWidth + spawnOffset,
                    Random.Range(camPos.y - camHeight, camPos.y + camHeight)
                );
                break;

            // вверх
            case 2:
                spawnPos = new Vector2(
                    Random.Range(camPos.x - camWidth, camPos.x + camWidth),
                    camPos.y + camHeight + spawnOffset
                );
                break;

            // низ
            case 3:
                spawnPos = new Vector2(
                    Random.Range(camPos.x - camWidth, camPos.x + camWidth),
                    camPos.y - camHeight - spawnOffset
                );
                break;
        }

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}*/