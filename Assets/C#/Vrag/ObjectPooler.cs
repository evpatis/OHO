using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    private Dictionary<GameObject, List<GameObject>> pools = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation, int defaultPoolSize = 20)
    {
        if (prefab == null) return null;

        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = new List<GameObject>();

            for (int i = 0; i < defaultPoolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pools[prefab].Add(obj);
            }
        }

        List<GameObject> pool = pools[prefab];

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                GameObject obj = pool[i];
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(prefab, position, rotation);
        newObj.SetActive(true);
        pool.Add(newObj);
        return newObj;
    }
}