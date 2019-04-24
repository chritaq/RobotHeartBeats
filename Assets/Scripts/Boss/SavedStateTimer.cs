using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedStateTimer : MonoBehaviour
{
    private float stateTimer = 0;

    private void Update()
    {
        if (stateTimer >= 0)
        {
            stateTimer -= Time.deltaTime;
        }
    }

    public float CheckSavedStateTimer()
    {
        return stateTimer;
    }

    public void SetNewSavedStateTimer(float newTime)
    {

        stateTimer = newTime;
    }
}
