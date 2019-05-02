using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    private bool winScreenActive = false;

    PlayerIndex player;
    GamePadState state;

    private void Start()
    {
        player = PlayerIndex.One;

    }

    [SerializeField]
    private GameObject winScreenCanvas;

    [SerializeField]
    private GameObject pauseController;

    private void Update()
    {
        if (winScreenActive)
        {
            Destroy(pauseController);
            state = GamePad.GetState(player, GamePadDeadZone.Circular);

            if (state.Buttons.Start == ButtonState.Pressed)
            {
                SceneManager.LoadScene(0);

            }
            if (state.Buttons.Back == ButtonState.Pressed)
            {
                Application.Quit();

            }
        }
    }


    public void StartWinTransition()
    {
        winScreenCanvas.SetActive(true);
        winScreenActive = true;
        //StartCoroutine("GameOver");
    }
}
