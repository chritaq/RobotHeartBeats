using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : MonoBehaviour
{
    [SerializeField]
    private float teleportTime;


    private float rendererSizeX;
    private float rendererSizeY;
    private float rendererStartSizeX;
    private float rendererStartSizeY;

    private void Start()
    {
        SetTeleportPositions();
        rendererStartSizeX = spriteRenderer.size.x;
        rendererStartSizeY = spriteRenderer.size.y;
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

    [SerializeField]
    SpriteRenderer spriteRenderer;


    public void TeleportStart()
    {
        rendererSizeX = rendererStartSizeX;
        rendererSizeY = rendererStartSizeY;
        spriteRenderer.size = new Vector2(rendererStartSizeX, rendererStartSizeY);
        StartCoroutine("TeleportStartAnimation");
        //TempFix
        
    }

    public bool animationRunning = false;
    private float animationRate = 0.01f;
    private IEnumerator TeleportStartAnimation()
    {
        animationRunning = true;
        while(rendererSizeX >= 0)
        {
            rendererSizeX -= 0.1f;
            spriteRenderer.size = new Vector2(rendererSizeX, rendererSizeY);
            yield return new WaitForSeconds(animationRate);
        }

        this.transform.position = new Vector2(10000f, 10000f);
        animationRunning = false;
        yield return null;
    }

    private int teleportArrayNumber = 0;
    public void TeleportEnd()
    {
        this.transform.position = teleportPositions[teleportArrayNumber];
        StartCoroutine("TeleportEndAnimation");
        SetNextTeleport();
    }

    private IEnumerator TeleportEndAnimation()
    {
        animationRunning = true;
        while (rendererSizeX < rendererStartSizeX)
        {
            rendererSizeX += 0.1f;
            spriteRenderer.size = new Vector2(rendererSizeX, rendererSizeY);
            yield return new WaitForSeconds(animationRate);
        }
        animationRunning = false;
        yield return null;
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
        while(animationRunning)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(teleportTimeAbility);
        TeleportEnd();
        yield return null;
    }
}
