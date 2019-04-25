using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : Ship
{
    [SerializeField]
    private float placeholderInvulnerableTime;

    [SerializeField]
    private float placeholderRestTime;


    [SerializeField]
    private float maxDistanceFromPlayer;

    BossState currentState;
    BossState returnedState;

    private string currentStateName;
    private Collider2D thisCollider;

    private void Start()
    {
        thisCollider = this.GetComponent<Collider2D>();
        currentState = new BossInvulnerableState();
        currentState.Enter(this);

        flashingFx = GetComponent<FlashingFX>();

        UpdateHealthUI();
        UpdateStateUI(currentState.stateName);
    }


    private void Update()
    {
        returnedState = currentState.Update(this, Time.deltaTime);
        CheckStateSwap();
    }


    private void CheckStateSwap()
    {
        if(returnedState != null)
        {
            currentState.Exit(this);
            currentState = returnedState;
            currentState.Enter(this);
            UpdateStateUI(currentState.stateName);
        }
    }


    public override void GetHit(int damage)
    {
        base.GetHit(damage);
        UpdateHealthUI();

        currentState.Exit(this);
        currentState = new BossDamagedState(); ;
        currentState.Enter(this);
        UpdateStateUI(currentState.stateName);
    }


    //Temp! Should be updated to healthbar!
    [SerializeField]
    private Text healthText;
    private void UpdateHealthUI()
    {
        healthText.text = "Enemy Health is: " + health;
    }


    [SerializeField]
    private Text stateText;
    private void UpdateStateUI(string currentStateName)
    {
        //stateText.text = "Enemy State: " + currentStateName;
    }


    public float GetPlaceholderInvulnerableTime()
    {
        return placeholderInvulnerableTime;
    }

    public float GetPlaceholderRestTime()
    {
        return placeholderRestTime;
    }


    public void TurnOnInvulnerable()
    {
        thisCollider.enabled = false;
    }


    public void TurnOffInvulnerable()
    {
        thisCollider.enabled = true;
    }


    [SerializeField]
    private GameObject player;
    public float DistanceFromPlayer()
    {
        //Renskriv!
        return Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y));
    }

    public bool playerWithinMaxDistance()
    {
        if(DistanceFromPlayer() <= maxDistanceFromPlayer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public CannonSpawner cannonSpawner;
    public BossTeleport bossTeleport;
    public SavedStateTimer savedStateTimer;
    public FlashingFX flashingFx;
    public ColorChange colorChange;
}
