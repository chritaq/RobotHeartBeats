using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class PlayerController : Ship
{

    private Camera cam;

    [SerializeField]
    private int playerNumber;

    PlayerIndex player;
    GamePadState state;

    private FlashingFX flashingFX;

    private Vector2 stickInput;

    void Start()
    {
        cam = Camera.main;
        flashingFX = GetComponent<FlashingFX>();
        //originalFireRate = fireRate;
        if (playerNumber == 1)
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

        if (state.Buttons.X == ButtonState.Pressed && isInvulnerable == false && dashCharges > 0)
        {
            StartCoroutine("Dash");
        }

        if(canMove)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }

        if (canMove && !isInvulnerable)
        {
            AimAndShoot();
        }

    }


    private bool canMove = true;
    [SerializeField] private float dashTime = 0.5f;
    [SerializeField] private float dashSpeed = 40f;

    private IEnumerator Dash()
    {
        dashCharges--;
        thisCollider.enabled = false;
        canMove = false;
        UpdateVelocity(this.transform.up, dashSpeed);
        yield return new WaitForSeconds(dashTime);
        thisCollider.enabled = true;
        canMove = true;
        yield return null;
    }

    private int dashCharges;
    [SerializeField]
    private int maxDashCharges;
    public void GetCharge()
    {
        Debug.Log("Charged Gotten");
        if(dashCharges < maxDashCharges)
        {
            dashCharges++;
        }
    }

    
    private bool shoulderHasBeenPressed = false; //Need to remove this!
    private void Update()
    {
        state = GamePad.GetState(player, GamePadDeadZone.Circular);
        //GamePad.GetState(playerIndex,);
        ClampPlayerToScreen();


        if(state.Buttons.RightShoulder == ButtonState.Pressed && !shoulderHasBeenPressed)
        {
            shoulderHasBeenPressed = true;
            ChangeColor();
            
        }
        if (state.Buttons.RightShoulder == ButtonState.Released && shoulderHasBeenPressed)
        {
            shoulderHasBeenPressed = false;
        }

        
        
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
            SwingWeapon();
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
    PlayerWeaponController playerWeapon;
    private void SwingWeapon()
    {
        playerWeapon.TrySwingWeapon();
    }

    private void ChangeColor()
    {
        playerWeapon.ChangeWeaponColor();
    }

    


    private float timeBeforeInvulnerableOff;
    [SerializeField]
    private float inVulnerableTime = 3f;

    public override void GetHit(int damage)
    {
        
        timeBeforeInvulnerableOff = Time.time + inVulnerableTime;
        AudioManager.instance.PlayPlayerDamaged();
        base.GetHit(damage);
        StartCoroutine("Invulnerable");
    }


    [SerializeField]
    private Collider2D thisCollider;
    private bool isInvulnerable = false;
    private IEnumerator Invulnerable()
    {
        isInvulnerable = true;
        thisCollider.enabled = false;
        flashingFX.StartConstantFlash();


        yield return new WaitForSeconds(inVulnerableTime);
        isInvulnerable = false;
        thisCollider.enabled = true;
        flashingFX.StopAllFlash();
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
