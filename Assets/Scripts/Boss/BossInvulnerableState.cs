using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInvulnerableState : BossState
{

    private float invulnerabilityTime;

    private SavedStateTimer savedStateTimer;

    public override void Enter(BossController bossController)
    {
        stateName = "Invulnerable";
        savedStateTimer = bossController.savedStateTimer;
        //invulnerabilityTime = bossController.GetPlaceholderInvulnerableTime();

        if (savedStateTimer.CheckSavedStateTimer() <= 0) {
            savedStateTimer.SetNewSavedStateTimer(bossController.GetPlaceholderInvulnerableTime());
        }

        invulnerabilityTime = savedStateTimer.CheckSavedStateTimer();

        bossController.TurnOnInvulnerable();
        
    }

    public override void Exit(BossController bossController)
    {
        
    }


    public override BossState Update(BossController bossController, float t)
    {

        invulnerabilityTime -= t;
        if(invulnerabilityTime <= 0)
        {
            bossController.TurnOffInvulnerable();
            return new BossAttackState();
        }

        if (bossController.playerWithinMaxDistance())
        {
            //Debug.Log("Player is too close!");
            return new BossTeleportState();
        }

        return null;

        
        //Om Spelaren kommer nära bossen
        //Boss.Teleport

    }
}
