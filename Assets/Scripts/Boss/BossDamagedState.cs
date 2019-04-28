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
        bossController.fullAttackStarter.StopFullAttack();
        bossController.TurnOnInvulnerable();

        bossController.flashingFx.StartConstantFlash();

        bossController.phaseController.DamagePhaseHealth();
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
            //Go To changePhaseState if phaseHealth == 0! 
            if(bossController.phaseController.phaseHealth[bossController.phaseController.activePhase] == 0)
            {
                //return new BossPhaseChangeState;

                //Temp code:
                bossController.phaseController.UpdateActivePhase();
            }

            return new BossInvulnerableState();
        }
        return null;
    }
}
