using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : MonoBehaviour
{
    [SerializeField]
    private float teleportTime;

    private void Start()
    {
        SetTeleportPositions();
    }



    private Vector2[] teleportPositions;
    [SerializeField]
    private Transform[] teleportTransforms;

    private void SetTeleportPositions()
    {
        teleportPositions = new Vector2[teleportTransforms.Length];
        for (int i = 0; i < teleportTransforms.Length; i++)
        {
            teleportPositions[i] = new Vector2(teleportTransforms[i].position.x, teleportTransforms[i].position.y);
        }
    }

    public void TeleportStart()
    {
        //TempFix
        this.transform.position = new Vector2(10000f, 10000f);
    }

    private int teleportArrayNumber = 0;
    public void TeleportEnd()
    {
        this.transform.position = teleportPositions[teleportArrayNumber];
        SetNextTeleport();
    }

    private void SetNextTeleport()
    {
        if (teleportArrayNumber < teleportPositions.Length - 1)
        {
            teleportArrayNumber++;
        }
        else
        {
            teleportArrayNumber = 0;
        }
    }

    public void TeleportEnd(Vector2 newTeleportPosition)
    {
        this.transform.position = newTeleportPosition;
    }

    public float GetTeleportTime()
    {
        return teleportTime;
    }


    private float teleportTimeAbility;
    public void StartTeleportAbility(float teleportTime)
    {
        teleportTimeAbility = teleportTime;
        StartCoroutine("Teleport");
    }

    private IEnumerator Teleport()
    {
        TeleportStart();
        yield return new WaitForSeconds(teleportTimeAbility);
        TeleportEnd();
        yield return null;
    }
}
