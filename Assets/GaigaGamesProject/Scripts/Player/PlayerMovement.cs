using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //https://www.youtube.com/watch?v=0-c3ErDzrh8
    // Movement
    [SerializeField] private Rigidbody2D body;

    public float speed = 3.0f;
    public float drag = 0.9f;
    public bool grounded = false;

    void Start()
    {
    }

    void Update()
    {

        // New input system
        float xInput = Input.GetAxis("Horizontal"); 
        float yInput = Input.GetAxis("Vertical");

        // Normalized makes diagonal same max speed as horizontal
        //Vector2 direction = new Vector2(xInput, yInput).normalized;
        //Vector2 direction = new Vector2(xInput, body.velocity.y).normalized;   
        //body.velocity = direction * speed;

        if (Mathf.Abs(xInput) > 0)
        {
            body.velocity = new Vector2(xInput * speed, body.velocity.y).normalized;
        }
        if (Mathf.Abs(yInput) > 0)
        {
            //body.velocity = new Vector2(body.velocity.x, yInput * speed).normalized;
        }
    }

    private void FixedUpdate()
    {
        //CheckGround();

        //if (grounded)
        //{
        //    body.velocity *= drag;
        //}
    }

    private void CheckGround()
    {
        throw new NotImplementedException();
    }
}
