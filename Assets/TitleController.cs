﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class TitleController : MonoBehaviour
{
    PlayerIndex player;
    GamePadState state;

    [SerializeField]
    private float transitionTime = 3;

    [SerializeField]
    private TextFlash pressStartFlash;

    private bool startWaitDone = false;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerIndex.One;
        StartCoroutine("StartWait");
        player = PlayerIndex.One;
    }

    private bool hasPressed = false;

    // Update is called once per frame
    void Update()
    {
        state = GamePad.GetState(player, GamePadDeadZone.Circular);
        if(state.Buttons.Start == ButtonState.Pressed && !hasPressed && startWaitDone)
        {
            hasPressed = true;
            StartCoroutine("SceneChange");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    [SerializeField]
    AudioSource startSound;

    [SerializeField]
    private SceneTransition sceneTransition;
    private IEnumerator SceneChange()
    {
        pressStartFlash.StartConstantFlash();
        startSound.Play();
        yield return new WaitForSeconds(transitionTime);
        sceneTransition.StartTransition();
        while(sceneTransition.isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
            
        }
        //ChangeScene;
        Debug.Log("ChangeScnee");
        SceneManager.LoadScene(1);
        yield return null;
    }

    private IEnumerator StartWait()
    {
        yield return new WaitForSeconds(1f);
        startWaitDone = true;
    }
    
}
