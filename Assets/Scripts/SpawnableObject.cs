using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    public Spawner spawner;
    public int poolId;

    public virtual void OnSpawn() { }
    public virtual void OnDespawn() { }
}
