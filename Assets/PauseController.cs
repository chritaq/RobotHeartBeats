using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    //Taken from https://answers.unity.com/questions/1230216/a-proper-way-to-pause-a-game.html

    PlayerIndex player;
    GamePadState state;

    [SerializeField] private GameObject pausePanel;
    void Start()
    {
        pausePanel.SetActive(false);
    }


    private bool hasPressed = false;

    void Update()
    {
        state = GamePad.GetState(player, GamePadDeadZone.Circular);
        if (state.Buttons.Start == ButtonState.Pressed && !hasPressed)
        {
            hasPressed = true;
            
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
            else if (pausePanel.activeInHierarchy)
            {
                ContinueGame();
            }
        }

        if (pausePanel.activeInHierarchy && state.Buttons.Back == ButtonState.Pressed)
        {
            Application.Quit();
        }

        if (state.Buttons.Start == ButtonState.Released)
        {
            hasPressed = false;
        }
    }
    private void PauseGame()
    {
        Debug.Log("Paused");
        Time.timeScale = 0;
        Debug.Log(Time.timeScale);
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }
}
