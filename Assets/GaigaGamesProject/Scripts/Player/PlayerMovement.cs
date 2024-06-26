using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //https://www.youtube.com/watch?v=0-c3ErDzrh8
    // https://www.youtube.com/watch?v=dwcT-Dch0bA
    // Movement
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private CharacterController2D characterController2D;

    public float speed = 10.0f;
    private bool jump = false;
    public float drag = 0.9f;
    public bool grounded = false;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;

    void Start()
    {
    }

    void Update()
    {

        // New input system
        horizontalMove = Input.GetAxis("Horizontal") * speed; 
        float yInput = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        // Normalized makes diagonal same max speed as horizontal
        //Vector2 direction = new Vector2(horizontalMove, yInput).normalized;
        //Vector2 direction = new Vector2(xInput, body.velocity.y).normalized;   
        //body.velocity = direction * speed;

        //if (Mathf.Abs(horizontalMove) > 0)
        //{
        //    body.velocity = new Vector2(xInput * speed, body.velocity.y).normalized;
        //}
        //if (Mathf.Abs(yInput) > 0)
        //{
        //    //body.velocity = new Vector2(body.velocity.x, yInput * speed).normalized;
        //}
    }

    private void FixedUpdate()
    {
        //CheckGround();

        //if (grounded)
        //{
        //    body.velocity *= drag;
        //}
        characterController2D.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

        //if (Mathf.Abs(horizontalMove) > 0)
        //{
        //    body.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime, body.velocity.y).normalized;
        //    //characterController2D.Move(horizontalMove * Time.fixedDeltaTime, false, false);
        //}

    }

    private void CheckGround()
    {
        throw new NotImplementedException();
    }
}
