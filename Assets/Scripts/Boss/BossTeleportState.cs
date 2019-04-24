using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleportState : BossState
{
    private float teleportTime;

    public override void Enter(BossController bossController)
    {
        stateName = "Teleport";
        teleportTime = bossController.bossTeleport.GetTeleportTime();
        bossController.TurnOnInvulnerable();
        bossController.bossTeleport.TeleportStart();
    }

    public override void Exit(BossController bossController)
    {
        
        bossController.TurnOffInvulnerable();
    }

    public override BossState Update(BossController bossController, float t)
    {
        teleportTime -= t;
        if (teleportTime <= 0)
        {
            bossController.bossTeleport.TeleportEnd();
            //Change this so it goes to last state
            return new BossInvulnerableState();
        }

        return null;
    }

}
