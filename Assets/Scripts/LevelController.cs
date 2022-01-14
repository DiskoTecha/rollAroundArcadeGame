using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Spawner[] objectiveSpawners;
    public float enemySpawnSpeedAddition;
    public int objectivesNeeded;

    protected int currentLevel;

    protected void Start()
    {
        currentLevel = 1;
        RespawnObjectives();
    }

    public virtual void LevelUp()
    {
        currentLevel++;
        RespawnObjectives();
    }

    protected void RespawnObjectives()
    {
        for (int i = 0; i < objectiveSpawners.Length; i++)
        {
            objectiveSpawners[i].DespawnAll();
        }
        for (int i = 0; i < objectivesNeeded; i++)
        {
            objectiveSpawners[Random.Range(0, objectiveSpawners.Length)].Spawn(6);
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
