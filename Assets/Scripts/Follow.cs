using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Follow : MonoBehaviour
{
    public Transform followObject;

    public float followDistance = 3.0f;
    public float followTiltAngle = 30.0f;
    public float localYOffset = 1.5f;
    public float mouseSensitivity = 1.0f;

    private float defaultFollowDistance;
    private float defaultFollowTiltAngle;
    private float defaultLocalYOffset;

    private float horizontalMouse;
    private float currentRotationAngle;

    private bool look;

    // Set the default position
    void Start()
    {
        transform.position = resetPosition();
        transform.LookAt(followObject);

        horizontalMouse = 0.0f;
        currentRotationAngle = 0.0f;

        look = false;

        defaultFollowDistance = followDistance;
        defaultFollowTiltAngle = followTiltAngle;
        defaultLocalYOffset = localYOffset;
    }

    // Read input
    void Update()
    {
        horizontalMouse = Input.GetAxis("Mouse X");
        if (Input.GetKeyDown(KeyCode.Period))
        {
            look = !look;
        }
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            
        }
    }

    // Move the camera due to player ball movement and input
    void LateUpdate()
    {
        currentRotationAngle += horizontalMouse * mouseSensitivity;
        transform.position = resetPosition();
        Vector3 difference = transform.position - followObject.position;
        difference = Quaternion.Euler(0, currentRotationAngle, 0) * difference + transform.up * localYOffset;  // Rotate the follow vector
        transform.LookAt(followObject);                          // Reface the object, to account for a dynamic followTiltAngle 
        transform.position = difference + followObject.position; // Move camera along it's relative y axis to offset it
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, currentRotationAngle + 180, transform.eulerAngles.z);  // Apply the y axis rotation
    }

    // Reset to default camera position/rotation
    Vector3 resetPosition()
    {
        return new Vector3(
            followObject.position.x,
            followObject.position.y + (float)(followDistance * Math.Sin(Math.PI / 180 * followTiltAngle)),
            followObject.position.z + (float)(followDistance * Math.Cos(Math.PI / 180 * followTiltAngle))
            );
    }

    public void ResetCamera()
    {
        followDistance = defaultFollowDistance;
        followTiltAngle = defaultFollowTiltAngle;
        localYOffset = defaultLocalYOffset;
    }
    
}
