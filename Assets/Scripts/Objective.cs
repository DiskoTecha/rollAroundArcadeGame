using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Objective : SpawnableObject
{
    public float rotationalVelocity = 60.0f; // degrees/second
    private PlayerController playerController;

    void OnEnable()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public override void OnSpawn()
    {
        GetComponent<Rigidbody>().AddTorque(new Vector3(0f, rotationalVelocity, 0f), ForceMode.VelocityChange);
    }

    public override void OnDespawn()
    {
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == playerController.gameObject)
        {
            spawner.Despawn(this);
            playerController.AddObjective();
        }
    }
}
