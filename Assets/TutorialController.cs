using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    PlayerIndex player;
    GamePadState state;

    private Vector2 stickInput;

    private enum TutorialState {Movement, Slicing, ChangeWeapon, HitCorrectBullet, ChargedBullet, Dash, SlowDown, Complete };

    private TutorialState tutorialState;

    private string tutorialStateName;

    [SerializeField]
    private Text tutorialText;
    [SerializeField]
    private Text counterText;

    [SerializeField]
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        counterText.text = "";
        tutorialState = TutorialState.Movement;
        tutorialStateName = "Move with Left Stick";

        player = PlayerIndex.One;
    }



    private bool rightShoulderHasBeenPressed = false;
    private bool xHasBeenPressed = false;

    // Update is called once per frame
    void Update()
    {
        


        tutorialText.text = tutorialStateName;
        state = GamePad.GetState(player, GamePadDeadZone.Circular);
        CheckMovement();
        if((moveHorizontal != 0 || moveVertical != 0) && tutorialState == TutorialState.Movement)
        {
            MovementCounter();
        }

        CheckSlicing();
        if ((sliceHorizontal != 0 || sliceVertical != 0) && tutorialState == TutorialState.Slicing)
        {
            SlicingCounter();
        }

        

        if (tutorialState == TutorialState.ChangeWeapon)
        {
            if (!rightShoulderHasBeenPressed && state.Buttons.RightShoulder == ButtonState.Pressed)
            {
                ChangeWeaponCounter();
                
                rightShoulderHasBeenPressed = true;
            }
            if (rightShoulderHasBeenPressed && state.Buttons.RightShoulder == ButtonState.Released)
            {
                rightShoulderHasBeenPressed = false;
            }
        }

        if(tutorialState == TutorialState.HitCorrectBullet)
        {
            SpawnBullets();
            if(sliceHorizontal != 0 || sliceVertical != 0)
            {
                HitCorrectBulletCounter();
            }
            
        }

        if (tutorialState == TutorialState.Dash)
        {
            SpawnChargedBullets();
            //if (state.Buttons.X == ButtonState.Pressed && playerController.hasCharges())
            //{
            //    DashCounter();
            //}

            if (!xHasBeenPressed && state.Buttons.X == ButtonState.Pressed && playerController.hasCharges())
            {
                DashCounter();
                xHasBeenPressed = true;
            }

            if (xHasBeenPressed && state.Buttons.X == ButtonState.Released)
            {
                xHasBeenPressed = false;
            }

        }

        if(tutorialState == TutorialState.SlowDown && state.Triggers.Left > 0)
        {
            SlowDownCounter();
        }

        if (tutorialState == TutorialState.Complete)
        {
            ChangeCompleteCounter();
        }

        //Debug.Log(tutorialStateName);
    }

    private float moveHorizontal;
    private float moveVertical;
    private Vector2 stickInputLeft;

    private void CheckMovement()
    {
        moveHorizontal = state.ThumbSticks.Left.X;
        moveVertical = state.ThumbSticks.Left.Y;
    }

    private float sliceHorizontal;
    private float sliceVertical;
    private void CheckSlicing()
    {
        sliceHorizontal = state.ThumbSticks.Right.X;
        sliceVertical = state.ThumbSticks.Right.Y;
    }

    float movementCounter = 400;
    private void MovementCounter ()
    {
        movementCounter--;
        counterText.text = "" + movementCounter;
        if (movementCounter <= 0)
        {
            tutorialState = TutorialState.Slicing;
            tutorialStateName = "Slice with right stick";
            counterText.text = "";
        }
    }

    float slicingCounter = 400;
    private void SlicingCounter()
    {
        slicingCounter--;
        counterText.text = "" + slicingCounter;
        if (slicingCounter <= 0)
        {
            counterText.text = "";
            tutorialState = TutorialState.ChangeWeapon;
            tutorialStateName = "Change weapon on Right Bumper";
        }
    }

    float changeWeaponCounter = 4;
    private void ChangeWeaponCounter()
    {
        changeWeaponCounter--;
        counterText.text = "" + changeWeaponCounter;
        if (changeWeaponCounter <= 0)
        {
            counterText.text = "";
            tutorialState = TutorialState.HitCorrectBullet;
            tutorialStateName = "Hit with correct color for extra damage";
            ResetSlicingCounter();
        }
    }

    private void ResetSlicingCounter()
    {
        slicingCounter = 1000;
    }

    private void HitCorrectBulletCounter()
    {
        slicingCounter--;
        //counterText.text = "" + slicingCounter;
        if (slicingCounter <= 0)
        {
            //counterText.text = "";
            tutorialState = TutorialState.Dash;
            tutorialStateName = "Hit Charged Bullets to gain a Dash-Charge. Use on X to Dash.";
        }
    }

    
    float dashingCounter = 4;
    private void DashCounter()
    {
        dashingCounter--;
        counterText.text = "" + dashingCounter;
        if (dashingCounter <= 0)
        {
            counterText.text = "";
            tutorialState = TutorialState.SlowDown;
            tutorialStateName = "Hold Left Trigger to slow down";
            
        }
    }

    float slowDownCounter = 400;
    private void SlowDownCounter()
    {
        slowDownCounter--;
        counterText.text = "" + slowDownCounter;
        if (slowDownCounter <= 0)
        {
            counterText.text = "";
            tutorialState = TutorialState.Complete;
            tutorialStateName = "Tutorial Complete";
        }
    }

    [SerializeField]
    private Transform orangeBullet;
    [SerializeField]
    private Transform blueBullet;


    private float nextShoot;
    private float shootRate = 2f;
    private void SpawnBullets()
    {
        if (Time.time > nextShoot)
        {
            Instantiate(orangeBullet, new Vector3(-10, 25, 0), Quaternion.identity);
            Instantiate(blueBullet, new Vector3(10, 25, 0), Quaternion.identity);
            nextShoot = Time.time + shootRate;
        }
        
    }

    [SerializeField]
    private Transform chargedBullet;

    private void SpawnChargedBullets()
    {
        if (Time.time > nextShoot)
        {
            Instantiate(chargedBullet, new Vector3(0, 25, 0), Quaternion.identity);
            nextShoot = Time.time + shootRate;
        }

    }



    float completeCounter = 300;
    private bool completePlayed = false;

    private void ChangeCompleteCounter()
    {
        completeCounter--;
        if (completeCounter <= 0 && !completePlayed)
        {

            StartCoroutine("SceneChange");
            completePlayed = true;
        }
    }

    [SerializeField]
    private SceneTransition sceneTransition;

    private float transitionTime = 2f;

    private IEnumerator SceneChange()
    {
        //startSound.Play();
        yield return new WaitForSeconds(transitionTime);
        sceneTransition.StartTransition();
        while (sceneTransition.isPlaying)
        {
            Debug.Log("is waiting");
            yield return new WaitForSeconds(0.1f);
        }
        //ChangeScene;
        Debug.Log("ChangeScnee");
        SceneManager.LoadScene(2);
        yield return null;
    }
}
