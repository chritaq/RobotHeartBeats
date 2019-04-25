using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamagedState : BossState
{

    
    private float damagedStateTime = 2f;

    public override void Enter(BossController bossController)
    {
        AudioManager.instance.PlayBossDamaged();
        stateName = "Damaged";
        bossController.cannonSpawner.StopFullAttack();
        bossController.TurnOnInvulnerable();

        bossController.flashingFx.StartConstantFlash();
    }

    public override void Exit(BossController bossController)
    {
        bossController.flashingFx.StopAllFlash();

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
