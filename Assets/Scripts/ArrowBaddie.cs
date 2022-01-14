using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBaddie : Baddie
{
    public float speedMin;
    public float speedMax;
    public Vector3 rotationAnglesMax;

    private Vector3 moveVector;

    new void Start()
    {
        base.Start();
        moveVector = Vector3.zero;
    }

    public override void OnSpawnImplementation()
    {
        moveVector = -1 * Vector3.Normalize(transform.position);
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

        movementSet = true;

        transform.LookAt(transform.position + moveVector);

        // Scale randomly
        Vector3 scale = new Vector3();
        float multiplier1 = Random.Range(0f, 1f);
        scale = scaleMin + (scaleMax - scaleMin) * multiplier1;
        //scale = scaleMin + new Vector3(
        //    (scaleMax.x - scaleMin.x) * Random.Range(0f, 1f),
        //    (scaleMax.y - scaleMin.y) * Random.Range(0f, 1f),
        //    (scaleMax.z - scaleMin.z) * Random.Range(0f, 1f));

        transform.localScale = scale;
    }
}
