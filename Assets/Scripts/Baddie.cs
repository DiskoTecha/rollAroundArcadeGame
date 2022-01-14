using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Baddie : SpawnableObject
{
    public Vector3 scaleMin;
    public Vector3 scaleMax;
    private PlayerController playerController;
    protected Vector3 spawnPosition;
    protected bool movementSet;

    protected void Start()
    {
        GetComponent<Collider>().isTrigger = true;

        spawnPosition = Vector3.negativeInfinity;
        movementSet = false;
    }

    void OnEnable()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == playerController.gameObject)
        {
            playerController.KillPlayer();
        }
    }

    public virtual void OnSpawnImplementation() { }

    public override void OnSpawn()
    {
        // Save position, for descendants to use for movement
        OnSpawnImplementation();
    }

    public override void OnDespawn()
    {
        // Reset spawnPosition
        spawnPosition = Vector3.negativeInfinity;

        // Reset movement so as not to add multiple velocities
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
