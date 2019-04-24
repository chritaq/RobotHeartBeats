using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSpawner : MovingPart, IAbilityStartable
{

    private GameObject[] enemyCannonClone;
    [SerializeField] private GameObject enemyCannon;

    private PatternAttack[] patternAttacks;
    [SerializeField] FullAttack fullAttack;

    private float[] timesBeforeAttacks;

    private void Start()
    {
        
        //patternAttackDatas = patternAttacks.patternAttackData;
        //StartCoroutine("SpawnCannons2");
        
    }

    public void StartAbility()
    {
        AddAttackArrayIndexes();
        GetAttackData();

        //StartCoroutine("FullAttackSpawner");
        StartCoroutine("PatternAttackSpawner");

    }

    private void AddAttackArrayIndexes()
    {
        patternAttacks = new PatternAttack[fullAttack.FullAttackData.Length];
        timesBeforeAttacks = new float[fullAttack.FullAttackData.Length];
    }

    private void GetAttackData()
    {
        for (int i = 0; i < fullAttack.FullAttackData.Length; i++)
        {
            patternAttacks[i] = fullAttack.FullAttackData[i].patternAttack;
            timesBeforeAttacks[i] = fullAttack.FullAttackData[i].timeBeforeAttack;
        }
    }


    //private IEnumerator FullAttackSpawner()
    //{

    //    //Goes through each patternattack in the full attack.
    //    for (int i = 0; i < patternAttacks.Length; i++)
    //    {
    //        yield return new WaitForSeconds(timesBeforeAttacks[i]);
    //        enemyCannonClone = new GameObject[patternAttacks[i].patternAttackData.Length];

    //        //Goes through each pattern in the patternattack.
    //        for (int j = 0; j < patternAttacks[i].patternAttackData.Length; j++)
    //        {

    //            yield return new WaitForSeconds(patternAttacks[i].patternAttackData[j].timeBeforeSpawn);

    //            SpawnCannonWithPattern(i, j);

    //            //Finishes the patternattack before starting the next attack if true.
    //            if (patternAttacks[i].patternAttackData[j].finnishAttack)
    //            {
    //                yield return new WaitForSeconds(patternAttacks[i].patternAttackData[j].pattern.patternTime);
    //            }

    //        }

    //    }

    //    yield return null;
    //}

    private bool attackFinished;

    public bool CheckAttackFinished()
    {
        return attackFinished;
    }

    private IEnumerator PatternAttackSpawner()
    {
        attackFinished = false;
        enemyCannonClone = new GameObject[patternAttacks[0].patternAttackData.Length];

        //Goes through each pattern in the patternattack.
        for (int j = 0; j < patternAttacks[0].patternAttackData.Length; j++)
        {

            yield return new WaitForSeconds(patternAttacks[0].patternAttackData[j].timeBeforeSpawn);

            SpawnCannonWithPattern(0, j);

            //Finishes the patternattack before starting the next attack if true.
            if (patternAttacks[0].patternAttackData[j].finnishAttack)
            {
                yield return new WaitForSeconds(patternAttacks[0].patternAttackData[j].pattern.patternTime);
            }

        }
        attackFinished = true;
        yield return null;
    }


    private void SpawnCannonWithPattern(int i, int j)
    {
        enemyCannon.GetComponent<EnemyCannonNew>().pattern = patternAttacks[i].patternAttackData[j].pattern;
        enemyCannonClone[j] = Instantiate(enemyCannon, this.transform);
    }

}
