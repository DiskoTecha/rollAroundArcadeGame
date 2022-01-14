using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ResetBarrier : MonoBehaviour
{
    public float xMin = -101f;
    public float xMax = 101f;
    public float yMin = -101f;
    public float yMax = 101f;
    public float zMin = -101f;
    public float zMax = 101f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.x < xMin || pos.x > xMax || pos.y < yMin || pos.y > yMax || pos.z < zMin || pos.z > zMax)
        {
            transform.position = GetComponent<PlayerController>().resetPosition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
