using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public PoolController poolController;

    // Boundaries of spawner object
    private Vector3 minBoundary;
    private Vector3 maxBoundary;

    // Lists to keep track of spawned objects
    private List<SpawnableObject>[] spawnedObjects;

    int totalRemoved = 0;

    void Awake()
    {
        minBoundary = transform.position - (transform.localScale / 2f);
        maxBoundary = transform.position + (transform.localScale / 2f);

        // Account for rotation by rotating points around the position of the spawner object
        Vector3 minDifference = -transform.localScale / 2f; // Vectors of difference between position of spawner and boundaries
        Vector3 maxDifference = transform.localScale / 2f;
        minDifference = transform.localRotation * minDifference;  // Rotate the difference vectors
        maxDifference = transform.localRotation * maxDifference;
        minBoundary = transform.position + minDifference; // Add rotated vectors to position of spawner
        maxBoundary = transform.position + maxDifference;

        spawnedObjects = new List<SpawnableObject>[poolController.objectsToPool.Length];
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            spawnedObjects[i] = new List<SpawnableObject>();
        }
    }

    public void Spawn(int poolId)
    {
        SpawnableObject instance = poolController.GetObject(poolId);
        instance.transform.localPosition = RandomInBoundary();

        instance.spawner = this;
        instance.OnSpawn();

        spawnedObjects[poolId].Add(instance);
    }

    private Vector3 RandomInBoundary()
    {
        return new Vector3(
            Random.Range(minBoundary.x, maxBoundary.x),
            Random.Range(minBoundary.y, maxBoundary.y),
            Random.Range(minBoundary.z, maxBoundary.z));
    }

    public void DespawnAll()
    {
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            for (int j = 0; j < spawnedObjects[i].Count; j++)
            {
                spawnedObjects[i][j].OnDespawn();
                poolController.ReturnObject(spawnedObjects[i][j].poolId, spawnedObjects[i][j]);
            }

            spawnedObjects[i].Clear();
        }
    }

    public void Despawn(SpawnableObject spawned)
    {
        int poolId = spawned.poolId;
        if (spawnedObjects[poolId].Contains(spawned))
        {
            // Remove from spawnedObjects listarray
            spawned.OnDespawn();
            poolController.ReturnObject(poolId, spawned);
            spawnedObjects[poolId].Remove(spawned);
            totalRemoved++;
        }
        else
        {
            Debug.LogError("spawnedObjects listarray does not contain object: " 
                + spawned.name + " with poolId " + poolId 
                + " in this spawner: " + gameObject.name);
        }
    }

    public void DespawnByPoolIds(params int[] poolIds)
    {
        for (int i = 0; i < poolIds.Length; i++)
        {
            DespawnByPoolId(poolIds[i]);
        }
    }

    private void DespawnByPoolId(int poolId)
    {
        for (int i = 0; i < spawnedObjects[poolId].Count; i++)
        {
            spawnedObjects[poolId][i].OnDespawn();
            poolController.ReturnObject(poolId, spawnedObjects[poolId][i]);
        }

        spawnedObjects[poolId].Clear();
    }
}
