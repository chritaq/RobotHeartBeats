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

    private enum TutorialState {Movement, Slicing, ChangeWeapon, HitCorrectBullet, ChargedBullet, Dash, Complete};

    private TutorialState tutorialState;

    private string tutorialStateName;

    [SerializeField]
    private Text tutorialText;

    // Start is called before the first frame update
    void Start()
    {
        tutorialState = TutorialState.Movement;
        tutorialStateName = "Move with Left Stick";

        player = PlayerIndex.One;
    }

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

        if(tutorialState == TutorialState.ChangeWeapon && state.Buttons.RightShoulder == ButtonState.Pressed)
        {
            ChangeWeaponCounter();
        }

        if(tutorialState == TutorialState.HitCorrectBullet)
        {
            SpawnBullets();
            if(sliceHorizontal != 0 || sliceVertical != 0)
            {
                HitCorrectBulletCounter();
            }
            
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

    float movementCounter = 500;
    private void MovementCounter ()
    {
        movementCounter--;
        Debug.Log(movementCounter);
        if(movementCounter <= 0)
        {
            tutorialState = TutorialState.Slicing;
            tutorialStateName = "Slice with right stick";
        }
    }

    float slicingCounter = 500;
    private void SlicingCounter()
    {
        slicingCounter--;
        if (slicingCounter <= 0)
        {
            tutorialState = TutorialState.ChangeWeapon;
            tutorialStateName = "Change weapon on Right Bumper";
        }
    }

    float changeWeaponCounter = 100;
    private void ChangeWeaponCounter()
    {
        changeWeaponCounter--;
        if (changeWeaponCounter <= 0)
        {
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
        if (slicingCounter <= 0)
        {
            tutorialState = TutorialState.Complete;
            tutorialStateName = "Tutorial Complete!";
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



    float completeCounter = 750;
    private void ChangeCompleteCounter()
    {
        completeCounter--;
        if (completeCounter <= 0)
        {

            StartCoroutine("SceneChange");
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
            yield return new WaitForSeconds(0.1f);
        }
        //ChangeScene;
        Debug.Log("ChangeScnee");
        SceneManager.LoadScene(2);
        yield return null;
    }
}
