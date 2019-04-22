using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPart : MonoBehaviour
{
    
    protected Vector2 movement;

    [SerializeField]
    protected Rigidbody2D rb;

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected Vector2 direction;


    protected void UpdateVelocity(Vector2 thisDirection)
    {
        movement = thisDirection;
        transform.up = movement;
        rb.velocity = movement * speed;
    }


    protected void UpdateVelocity(Vector2 thisDirection, float newSpeed)
    {
        movement = thisDirection;
        transform.up = movement;
        rb.velocity = movement * newSpeed;
    }


    protected void UpdateDirection(Vector2 newDirection)
    {

        direction = new Vector2(newDirection.x, newDirection.y);
        transform.up = direction;
    }
}
