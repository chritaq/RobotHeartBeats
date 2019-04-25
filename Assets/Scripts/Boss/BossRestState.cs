using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRestState : BossState
{
    private float restTime;

    public override void Enter(BossController bossController)
    {
        stateName = "Resting";
        restTime = bossController.GetPlaceholderRestTime();
    }

    public override void Exit(BossController bossController)
    {
        
    }

    public override BossState Update(BossController bossController, float t)
    {
        restTime -= t;
        if (restTime <= 0)
        {
            return new BossInvulnerableState();
        }
        return null;
    }

    
}
