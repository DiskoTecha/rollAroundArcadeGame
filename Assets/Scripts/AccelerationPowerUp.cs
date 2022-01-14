using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPowerUp : PowerUp
{
    protected override void DeliverPayload()
    {
        playerController.acceleration += 10;
    }

    protected override void CleanupPayload()
    {
        playerController.acceleration -= 10;
    }
}
