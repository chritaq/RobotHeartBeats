using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceTest : MonoBehaviour
{
    public GameObject gameObject;

    private void Start()
    {
        gameObject.SendMessage("StartAbility");
    }
}
