using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnableObject))]
public class DespawnBarrier : MonoBehaviour
{
    public float xMin = -120f;
    public float xMax = 120f;
    public float yMin = -10f;
    public float yMax = 25f;
    public float zMin = -120f;
    public float zMax = 120f;

    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.x < xMin || pos.x > xMax || pos.y < yMin || pos.y > yMax || pos.z < zMin || pos.z > zMax)
        {
            Spawner spawner = GetComponent<SpawnableObject>().spawner;
            if (spawner != null)
            {
                spawner.Despawn(GetComponent<SpawnableObject>());
            }
        }
    }
}
