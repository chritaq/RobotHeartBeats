using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossState
{

    private int activePhase;
    private int activeAttack;

    public override void Enter(BossController bossController)
    {
        stateName = "Attack";
        activePhase = bossController.phaseController.activePhase;
        activeAttack = bossController.phaseController.activeAttack;

        bossController.fullAttackStarter.fullAttack = bossController.phaseController.phases[activePhase].phaseData.fullAttacks[activeAttack]; //Change 0 to the active attack!
        bossController.fullAttackStarter.SetupAbility();
        bossController.TurnOffInvulnerable();
    }

    public override void Exit(BossController bossController)
    {
        bossController.phaseController.UpdateActiveAttack();
        


    }

    public override BossState Update(BossController bossController, float t)
    {
        
        if (bossController.fullAttackStarter.CheckFullAttackFinished())
        {
            return new BossRestState();
        }
        bossController.fullAttackStarter.AbilityStarter();
        return null;
    }

}
