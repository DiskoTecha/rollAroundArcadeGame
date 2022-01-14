using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPowerUp : PowerUp
{
    private Follow followCam;
    private float saveFollowDistance;
    private float saveFollowTiltAngle;
    private float saveLocalYOffset;

    protected override void DeliverPayload()
    {
        followCam = playerController.followCamera.gameObject.GetComponent<Follow>();

        saveFollowDistance = followCam.followDistance;
        saveFollowTiltAngle = followCam.followTiltAngle;
        saveLocalYOffset = followCam.localYOffset;

        followCam.followDistance = 50.0f;
        followCam.followTiltAngle = 89.0f;
        followCam.localYOffset = 0.0f;
    }

    protected override void CleanupPayload()
    {
        followCam.followDistance = saveFollowDistance;
        followCam.followTiltAngle = saveFollowTiltAngle;
        followCam.localYOffset = saveLocalYOffset;
    }
}
