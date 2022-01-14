using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    public SpawnableObject[] objectsToPool;
    public int refillAmount = 5;

    private List<SpawnableObject>[] pools;

    void Start()
    {
        pools = new List<SpawnableObject>[objectsToPool.Length];

        // Create the parents for the pools
        for (int i = 0; i < objectsToPool.Length; i++)
        {
            GameObject pool = new GameObject(objectsToPool[i].gameObject.name + " Pool");
            pool.transform.parent = transform;
            pools[i] = new List<SpawnableObject>();
            RefillPool(i);
        }
    }

    public SpawnableObject GetObject(int poolIndex)
    {
        // Check if pool is empty
        int objectIndex = pools[poolIndex].Count - 1;
        if (objectIndex < 0)
        {
            RefillPool(poolIndex);
            objectIndex = refillAmount - 1;
        }

        SpawnableObject instance = pools[poolIndex][objectIndex];
        instance.gameObject.SetActive(true);
        pools[poolIndex].RemoveAt(objectIndex);

        return instance;
    }

    public void ReturnObject(int poolIndex, SpawnableObject instance)
    {
        instance.gameObject.SetActive(false);
        pools[poolIndex].Add(instance);
    }

    private void RefillPool(int poolIndex)
    {
        // Fill pool with specified amount
        for (int i = 0; i < refillAmount; i++)
        {
            GameObject instance = Instantiate(objectsToPool[poolIndex].gameObject);
            instance.SetActive(false);
            instance.transform.parent = transform.GetChild(poolIndex);
            pools[poolIndex].Add(instance.GetComponent<SpawnableObject>());
        }
    }
}
