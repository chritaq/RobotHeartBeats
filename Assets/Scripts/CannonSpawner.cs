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

        StartCoroutine("FullAttackSpawner");
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


    private IEnumerator FullAttackSpawner()
    {

        //Goes through each patternattack in the full attack.
        for (int i = 0; i < patternAttacks.Length; i++)
        {
            yield return new WaitForSeconds(timesBeforeAttacks[i]);
            enemyCannonClone = new GameObject[patternAttacks[i].patternAttackData.Length];

            //Goes through each pattern in the patternattack.
            for (int j = 0; j < patternAttacks[i].patternAttackData.Length; j++)
            {

                yield return new WaitForSeconds(patternAttacks[i].patternAttackData[j].timeBeforeSpawn);

                SpawnCannonWithPattern(i, j);

                //Finishes the patternattack before starting the next attack if true.
                if (patternAttacks[i].patternAttackData[j].finnishAttack)
                {
                    yield return new WaitForSeconds(patternAttacks[i].patternAttackData[j].pattern.patternTime);
                }

            }
            
        }

        yield return null;
    }

    private void SpawnCannonWithPattern(int i, int j)
    {
        enemyCannon.GetComponent<EnemyCannonNew>().pattern = patternAttacks[i].patternAttackData[j].pattern;
        enemyCannonClone[j] = Instantiate(enemyCannon, this.transform);
    }









    //OldStuff
    //private IEnumerator SpawnCannons()
    //{
        
    //    for(int i = 0; i < patternAttacks.Length; i++)
    //    {
            
    //        enemyCannonClone = new GameObject[patternAttacks[i].patternAttackData.Length];

    //        for (int j = 0; j < patternAttacks[i].patternAttackData.Length; j++)
    //        {
    //            if (j != 0 && patternAttacks[i].patternAttackData[j].timeBeforeSpawn != 0)
    //            {
    //                yield return new WaitForSeconds(patternAttacks[i].patternAttackData[j].timeBeforeSpawn);
    //            }

    //            enemyCannon.GetComponent<EnemyCannonNew>().pattern = patternAttacks[i].patternAttackData[j].pattern;
    //            enemyCannonClone[j] = Instantiate(enemyCannon, this.transform);


    //            if (patternAttacks[i].patternAttackData[j].finnishAttack)
    //            {
    //                yield return new WaitForSeconds(patternAttacks[i].patternAttackData[j].pattern.patternTime);
    //            }
    //        }
    //        yield return new WaitForSeconds(timesBeforeAttacks[i]);
    //    }

    //    yield return null;
    //}

}
