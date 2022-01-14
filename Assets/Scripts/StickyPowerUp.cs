using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPowerUp : PowerUp
{
    private float savedStickyStrength;

    protected override void DeliverPayload()
    {
        savedStickyStrength = playerController.stickyStrength;
        playerController.stickyStrength = 0.3f;
    }

    protected override void CleanupPayload()
    {
        playerController.stickyStrength = savedStickyStrength;
    }
}
