using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossState
{
    public override void Enter(BossController bossController)
    {
        stateName = "Attack";
        bossController.cannonSpawner.StartAbility();
        bossController.TurnOffInvulnerable();
    }

    public override void Exit(BossController bossController)
    {
        
        

    }

    public override BossState Update(BossController bossController, float t)
    {
        if (bossController.cannonSpawner.CheckAttackFinished())
        {
            return new BossRestState();
        }
        return null;
    }

}
