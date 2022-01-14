using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : PowerUp
{
    protected override void DeliverPayload()
    {
        playerController.jumpStrength += 6.0f;
    }

    protected override void CleanupPayload()
    {
        playerController.jumpStrength -= 6.0f;
    }
}
