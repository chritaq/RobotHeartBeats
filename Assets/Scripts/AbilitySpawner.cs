using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilitySpawner : MovingPart, IAbilityStartable
{

    private GameObject[] enemyCannonClone;
    [SerializeField] private GameObject enemyCannon;

    //private PatternAttack[] patternAttacks;

    private PatternAttack tempPatternAttack;
    private TeleportAbility tempTeleport;

    private Ability[] abilities;

    [SerializeField] FullAttack fullAttack;

    private float[] timesBeforeAbilities;

    private void Start()
    {
        
        //patternAttackDatas = patternAttacks.patternAttackData;
        //StartCoroutine("SpawnCannons2");
        
    }

    public void SetupAbility()
    {
        activeAbility = 0;
        fullAttackFinished = false;
        abilityFinished = true;
        AddAbilityArrayIndexes();
        GetAbilityData();
        

        //StartCoroutine("FullAttackSpawner");
        //AbilityStarter();

    }

    private void AddAbilityArrayIndexes()
    {
        //patternAttacks = new PatternAttack[fullAttack.FullAttackData.Length];
        //timesBeforeAttacks = new float[fullAttack.FullAttackData.Length];

        abilities = new Ability[fullAttack.FullAttackData.Length];
        timesBeforeAbilities = new float[fullAttack.FullAttackData.Length];
    }

    private void GetAbilityData()
    {
        for (int i = 0; i < fullAttack.FullAttackData.Length; i++)
        {
            abilities[i] = fullAttack.FullAttackData[i].bossAbility;
            timesBeforeAbilities[i] = fullAttack.FullAttackData[i].timeBeforeAbility;
        }

    }


    int activeAbility = 0;
    private bool abilityFinished;
    private bool fullAttackFinished;
    public void AbilityStarter()
    {
        if(abilityFinished)
        {
            StartNextAbility();
            Debug.Log("Started ability");
        }
    }


    private void StartNextAbility()
    {
        try
        {
            tempPatternAttack = (PatternAttack)abilities[activeAbility];
            StartCoroutine("PatternAttackSpawner");
        }
        catch
        {
            try
            {
                tempTeleport = (TeleportAbility)abilities[activeAbility];
                StartCoroutine("Teleport");
                
            }
            catch
            {
                Debug.Log("no ability worked");
            }
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

    

    public bool CheckAbilityFinished()
    {
        return abilityFinished;
    }

    private IEnumerator PatternAttackSpawner()
    {
        abilityFinished = false;
        enemyCannonClone = new GameObject[tempPatternAttack.patternAttackData.Length];

        yield return new WaitForSeconds(timesBeforeAbilities[activeAbility]);

        //Goes through each pattern in the patternattack.
        for (int i = 0; i < tempPatternAttack.patternAttackData.Length; i++)
        {

            yield return new WaitForSeconds(tempPatternAttack.patternAttackData[i].timeBeforeSpawn);

            SpawnCannonWithPattern(i);

            //Finishes the patternattack before starting the next attack if true.
            if (tempPatternAttack.patternAttackData[i].finnishAttack)
            {
                yield return new WaitForSeconds(tempPatternAttack.patternAttackData[i].pattern.patternTime);
            }

        }

        TryFinnishFullAttack();
        activeAbility++;

        yield return null;
    }


    private void SpawnCannonWithPattern(int i)
    {
        enemyCannon.GetComponent<EnemyCannonNew>().pattern = tempPatternAttack.patternAttackData[i].pattern;
        enemyCannonClone[i] = Instantiate(enemyCannon, this.transform);
    }


    private void TryFinnishFullAttack()
    {
        if (activeAbility == abilities.Length - 1)
        {
            fullAttackFinished = true;
        }
        else
        {
            abilityFinished = true;
        }
    }


    public bool CheckFullAttackFinished()
    {
        return fullAttackFinished;
    }



    public void StopFullAttack()
    {
        fullAttackFinished = true;
        StopAllCoroutines();
        DestroyAllCannons();
    }

    private GameObject[] cannonsToDestroy;
    private void DestroyAllCannons()
    {
        cannonsToDestroy = GameObject.FindGameObjectsWithTag("EnemyCannon");

        for (var i = 0; i < cannonsToDestroy.Length; i++)
        {
            Destroy(cannonsToDestroy[i]);
        }
    }

    [SerializeField]
    BossTeleport bossTeleport;
    private IEnumerator Teleport()
    {
        abilityFinished = false;
        for (int i = 0; i < tempTeleport.timesToTeleport; i++)
        {
            bossTeleport.StartTeleportAbility(tempTeleport.timeForEachTeleport);
            yield return new WaitForSeconds(tempTeleport.timeForEachTeleport);
            yield return new WaitForSeconds(tempTeleport.timeBetweenTeleports);
        }

        TryFinnishFullAttack();
        activeAbility++;

        yield return null;
    }
}
