using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TeleportAbility", menuName = "TeleportAbility")]
public class TeleportAbility : Ability
{
    //Simple version
    public float timeForEachTeleport;
    public float timeBetweenTeleports;
    public int timesToTeleport;
}
