using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class PlayerController : Ship
{

    private Camera cam;

    [SerializeField]
    private BulletAbsorber bulletAbsorber;

    

    [SerializeField]
    private int playerNumber;

    PlayerIndex player;
    GamePadState state;

    private Vector2 stickInput;

    void Start()
    {
        cam = Camera.main;
        //originalFireRate = fireRate;
        if(playerNumber == 1)
        {
            player = PlayerIndex.One;
        }
        if (playerNumber == 2)
        {
            player = PlayerIndex.Two;
        }
        if (playerNumber == 3)
        {
            player = PlayerIndex.Three;
        }
        if (playerNumber == 4)
        {
            player = PlayerIndex.Four;
        }
        
        
        
    }


    [SerializeField]
    private float triggerDeadZone = 0.1f;

    private void FixedUpdate()
    {
        Move();
        AimAndShoot();

        if (state.Buttons.X == ButtonState.Pressed || state.Triggers.Right > triggerDeadZone && !bulletAbsorber.isAbsorbing)
        {
            bulletAbsorber.ActivateBulletAbsorb();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }

        
    }


    

    private void Update()
    {
        state = GamePad.GetState(player, GamePadDeadZone.Circular);
        //GamePad.GetState(playerIndex,);
        ClampPlayerToScreen();
    }


    
    private void ClampPlayerToScreen()
    {
        Vector3 pos = cam.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = cam.ViewportToWorldPoint(pos);
    }

    

    

    private void Move() {

        UpdateMovementVector();

        //if(Input.GetButton("SlowDown"))
        //{
        //    SlowMovement();
        //}

        if (state.Triggers.Left > triggerDeadZone)
        {
            SlowMovement();
        }

        else
        {
            NormalMovement();
        }

        
        
    }

    private float moveHorizontal;
    private float moveVertical;
    private Vector2 stickInputLeft;

    private void UpdateMovementVector()
    {
        moveHorizontal = state.ThumbSticks.Left.X;
        moveVertical = state.ThumbSticks.Left.Y;
        stickInputLeft = new Vector2(moveHorizontal, moveVertical);
    }



    [SerializeField] private SpriteRenderer colliderGraphics;
    [SerializeField] protected float slowDownSpeed;

    private void SlowMovement()
    {
        if (colliderGraphics.enabled == false)
        {
            colliderGraphics.enabled = true;
        }
        UpdateVelocity(stickInputLeft, slowDownSpeed);
    }


    private void NormalMovement()
    {
        if (colliderGraphics.enabled == true)
        {
            colliderGraphics.enabled = false;
        }
        UpdateVelocity(stickInputLeft);
    }




    private void AimAndShoot()
    {

        if(state.ThumbSticks.Right.X != 0 || state.ThumbSticks.Right.Y != 0)
        {
            Aim();

            if(!bulletAbsorber.isAbsorbing)
            {
                Fire();
            }
            
        }
    }


    private Vector3 mousePosition;
    private Vector2 rightStickDirection;
    private float directionHorizontal;
    private float directionVertical;
    
     
    private void Aim() {

        directionHorizontal = state.ThumbSticks.Right.X;
        directionVertical = state.ThumbSticks.Right.Y;
        rightStickDirection = new Vector2(directionHorizontal, directionVertical);
        //rightStickDirection = DeadZone(rightStickDirection);

        UpdateDirection(rightStickDirection);
        
    }


    

    [SerializeField]
    PlayerCannon cannon;

    //private float nextFire;

    //[SerializeField]
    //private float fireRate = 0.1f;

    private void Fire()
    {
        cannon.Fire();
    }


    private float timeBeforeInvulnerableOff;
    [SerializeField]
    private float inVulnerableTime = 3f;

    public override void HitShip(int bulletDamage)
    {

        if (!bulletAbsorber.isAbsorbing && Time.time > timeBeforeInvulnerableOff)
        {
            timeBeforeInvulnerableOff = Time.time + inVulnerableTime;
            base.HitShip(bulletDamage);
            StartCoroutine("Invulnerable");
        }
    }


    [SerializeField]
    private Collider2D thisCollider;
    private IEnumerator Invulnerable()
    {
        thisCollider.enabled = false;
        yield return new WaitForSeconds(inVulnerableTime);
        thisCollider.enabled = true;
        yield return null;
    }

    [SerializeField]
    private float analogDeadZone = 0.25f;
    private Vector2 DeadZone(Vector2 thisStickInput)
    {
        
        if (thisStickInput.magnitude < analogDeadZone)
            thisStickInput = Vector2.zero;

        //else
        //    thisStickInput = thisStickInput.normalized * ((thisStickInput.magnitude - analogDeadZone) / (1 - analogDeadZone));
        return thisStickInput;
    }
}
