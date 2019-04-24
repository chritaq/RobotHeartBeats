using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    public string stateName;

    public abstract void Enter(BossController bossController);
    public abstract void Exit(BossController bossController);
    public abstract BossState Update(BossController bossController, float t);

}
