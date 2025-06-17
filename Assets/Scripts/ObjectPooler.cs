using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag '{tag}' does not exist.");
            return null;
        }

        Queue<GameObject> poolQueue = poolDictionary[tag];

        foreach (GameObject obj in poolQueue)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                return obj;
            }
        }

        Pool pool = pools.Find(p => p.tag == tag);
        if (pool != null)
        {
            GameObject newObj = Instantiate(pool.prefab, position, rotation);
            newObj.transform.SetParent(transform);
            poolQueue.Enqueue(newObj);
            return newObj;
        }

        return null;
    }


    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}