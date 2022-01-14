using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : LevelController
{
    public Spawner[] outerSpawners;
    public float enemySpawnSpeed = 3f; // Objects per second
    public float powerUpSpawnChance = 0.1f; // Checked at speed of enemy spawn speed

    private float timer;
    private float defaultEnemySpawnSpeed;

    new void Start()
    {
        base.Start();
        timer = 0f;
        defaultEnemySpawnSpeed = enemySpawnSpeed;
    }

    void Update()
    {
        if (timer >= 1f / enemySpawnSpeed)
        {
            outerSpawners[Random.Range(0, outerSpawners.Length)].Spawn(Random.Range(0, 2));
            timer -= 1f / enemySpawnSpeed;

            // Check for powerUp spawn
            float randomFloat = Random.Range(0f, 1f);
            if (randomFloat <= powerUpSpawnChance)
            {
                outerSpawners[Random.Range(0, outerSpawners.Length)].Spawn(Random.Range(2, 6));
            }
        }

        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.C))
        {
            ResetBaddies();
        }
    }

    public void ResetGame()
    {
        for (int i = 0; i < outerSpawners.Length; i++)
        {
            outerSpawners[i].DespawnAll();
        }

        RespawnObjectives();
        enemySpawnSpeed = defaultEnemySpawnSpeed;
        currentLevel = 1;
    }

    public void ResetBaddies()
    {
        for (int i = 0; i < outerSpawners.Length; i++)
        {
            outerSpawners[i].DespawnByPoolIds(0, 1);
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();
        ResetBaddies();
        enemySpawnSpeed = defaultEnemySpawnSpeed + currentLevel * enemySpawnSpeedAddition;
    }
}
