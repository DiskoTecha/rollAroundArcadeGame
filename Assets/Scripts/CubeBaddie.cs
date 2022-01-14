using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBaddie : Baddie
{
    public float speedMin;
    public float speedMax;

    private Vector3 moveVector;

    new void Start()
    {
        base.Start();
        moveVector = Vector3.zero;
    }

    public override void OnSpawnImplementation()
    {
        if (transform.position.x >= 100)
        {
            moveVector = new Vector3(-1, 0, 0);
        }
        else if (transform.position.x <= -100)
        {
            moveVector = new Vector3(1, 0, 0);
        }
        else if (transform.position.z >= 100)
        {
            moveVector = new Vector3(0, 0, -1);
        }
        else if (transform.position.z <= -100)
        {
            moveVector = new Vector3(0, 0, 1);
        }


        // Add random velocity to object in direction of moveVector
        GetComponent<Rigidbody>().AddForce(moveVector * Random.Range(speedMin, speedMax), ForceMode.VelocityChange);

        movementSet = true;

        // Scale randomly
        Vector3 scale = new Vector3();
        float multiplier1 = Random.Range(0f, 1f);
        scale = scaleMin + (scaleMax - scaleMin) * multiplier1;

        transform.localScale = scale;
    }
}
