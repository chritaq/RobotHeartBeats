﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossState
{
    public override void Enter(BossController bossController)
    {
        stateName = "Attack";
        bossController.cannonSpawner.SetupAbility();
        bossController.TurnOffInvulnerable();
    }

    public override void Exit(BossController bossController)
    {
        
        

    }

    public override BossState Update(BossController bossController, float t)
    {
        
        if (bossController.cannonSpawner.CheckFullAttackFinished())
        {
            return new BossRestState();
        }
        bossController.cannonSpawner.AbilityStarter();
        return null;
    }

}