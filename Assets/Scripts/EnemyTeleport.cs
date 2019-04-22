using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenTeleport = 5f;

    private Vector2[] teleportPositions;

    [SerializeField]
    private Transform[] teleportTransforms;

    private void Start()
    {
        SetTeleportPositions();
        InvokeRepeating("Teleport", 0, timeBetweenTeleport);
    }

    private void SetTeleportPositions()
    {
        teleportPositions = new Vector2[teleportTransforms.Length];
        for (int i = 0; i < teleportTransforms.Length; i++)
        {
            teleportPositions[i] = new Vector2(teleportTransforms[i].position.x, teleportTransforms[i].position.y);
        }
    }


    [SerializeField]
    private GameObject enemy;

    private int teleportArrayNumber = 0;

    public void Teleport()
    {
        enemy.transform.position = teleportPositions[teleportArrayNumber];
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


}
