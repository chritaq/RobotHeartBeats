using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    public int activePhase;
    public int activeAttack;
    [SerializeField]
    public Phase[] phases;

    public int[] phaseHealth;

    private void Start()
    {
        phaseHealth = new int[phases.Length];
        for (int i = 0; i < phases.Length; i++)
        {
            phaseHealth[i] = phases[i].phaseData.healthDuringPhase;
        }
    }


    public void UpdateActiveAttack()
    {
        if(activeAttack >= phases[activePhase].phaseData.fullAttacks.Length - 1)
        {
            activeAttack = 0; //Resets the active attack to 0
        }
        else
        {
            activeAttack++;
        }
    }

    public void UpdateActivePhase()
    {
        activePhase++;
    }

    public void DamagePhaseHealth()
    {
        phaseHealth[activePhase]--;
    }
}
