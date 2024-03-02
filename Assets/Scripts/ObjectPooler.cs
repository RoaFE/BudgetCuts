using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<PoolObject> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;

    void Awake()
    {
        Instance = this;
        Initialise();
    }

    void Initialise()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (PoolObject pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int i = 0; i < pool.count; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        List<GameObject> objectsToSpawn = poolDictionary[tag];
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            if (!objectsToSpawn[i].activeInHierarchy)
            {
                GameObject objectToSpawn = objectsToSpawn[i];
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;

                return objectToSpawn;
            }
        }
        return null;
    }

    
}


[System.Serializable]
[CreateAssetMenu(fileName = "PoolObject", menuName = "BudgetDefence/PoolObject", order = 0)]
public class PoolObject : ScriptableObject
{
    public string tag;
    public GameObject prefab;
    public int count;
    }