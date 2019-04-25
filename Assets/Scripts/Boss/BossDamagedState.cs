using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamagedState : BossState
{

    
    private float damagedStateTime = 2f;

    public override void Enter(BossController bossController)
    {
        stateName = "Damaged";
        bossController.cannonSpawner.StopFullAttack();
        bossController.TurnOnInvulnerable();
    }

    public override void Exit(BossController bossController)
    {

        
    }

    public override BossState Update(BossController bossController, float t)
    {
        damagedStateTime -= t;
        if(damagedStateTime <= 0)
        {
            return new BossInvulnerableState();
        }
        return null;
    }
}
