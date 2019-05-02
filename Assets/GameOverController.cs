using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private bool gameOverActive = false;

    PlayerIndex player;
    GamePadState state;

    private void Start()
    {
        player = PlayerIndex.One;

    }

    [SerializeField]
    private GameObject gameOverCanvas;

    [SerializeField]
    private GameObject pauseController;

    private void Update()
    {
        if(gameOverActive)
        {
            Destroy(pauseController);
            state = GamePad.GetState(player, GamePadDeadZone.Circular);

            if(state.Buttons.Start == ButtonState.Pressed)
            {
                SceneManager.LoadScene(2);

            }
            if (state.Buttons.Back == ButtonState.Pressed)
            {
                Application.Quit();

            }
        }
    }


    public void StartGameOverTransition()
    {
        gameOverCanvas.SetActive(true);
        gameOverActive = true;
        //StartCoroutine("GameOver");
    }

    //private IEnumerator GameOver()
    //{
    //    gameOverActive = true;
    //    yield return null;
    //}
}
