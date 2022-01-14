using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Light))]
public class PowerUp : SpawnableObject
{
    public PlayerController playerController;
    public float duration = 10.0f;
    public bool stationary = false;
    public Vector3 rotationAnglesMax;
    public float speedMin;
    public float speedMax;

    private float currentTime;
    private bool timing;
    private float defaultDuration;

    void Start()
    {
        currentTime = 0.0f;
        timing = false;
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = false;
        defaultDuration = duration;
    }

    void OnEnable()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (timing)
        {
            currentTime += Time.deltaTime;
        }

        if (currentTime >= duration)
        {
            timing = false;
            currentTime = 0.0f;
            CleanupPayload();
            spawner.Despawn(this);
        }
    }

    protected virtual void DeliverPayload() { }
    protected virtual void CleanupPayload() { }

    void OnTriggerEnter(Collider collider)
    {
        if (!timing && collider.gameObject == playerController.gameObject)
        {
            if (playerController.AddPowerUp(this))
            {
                DeliverPayload();
                timing = true;

                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                GetComponent<Light>().enabled = false;

                // Zero velocity so as not to despawn it by the despawn barrier before the payload is cleaned up
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            else
            {
                spawner.Despawn(this);
            }
        }
    }

    public override void OnSpawn()
    {
        if (!stationary)
        {
            Vector3 moveVector = -1 * Vector3.Normalize(transform.position);
            moveVector.y = 0f;

            // Rotate move vector randomly
            Vector3 diff = moveVector - transform.position;
            Quaternion quaternion = Quaternion.Euler(
                Random.Range(-rotationAnglesMax.x, rotationAnglesMax.x),
                Random.Range(-rotationAnglesMax.y, rotationAnglesMax.y),
                Random.Range(-rotationAnglesMax.z, rotationAnglesMax.z));

            moveVector = quaternion * moveVector;

            // Add random velocity to object in direction of moveVector
            GetComponent<Rigidbody>().AddForce(moveVector * Random.Range(speedMin, speedMax), ForceMode.VelocityChange);
        }
    }

    public override void OnDespawn()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        GetComponent<Light>().enabled = true;

        timing = false;
        currentTime = 0.0f;
    }

    public float AddDuration()
    {
        duration += defaultDuration;
        return duration;
    }
}
