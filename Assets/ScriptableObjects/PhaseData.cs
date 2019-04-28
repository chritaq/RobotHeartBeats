using UnityEngine;

[System.Serializable]
public class PhaseData : object
{
    public FullAttack[] fullAttacks;
    public int healthDuringPhase;
    public float invulnerableTimeBetweenAttacks;
}
