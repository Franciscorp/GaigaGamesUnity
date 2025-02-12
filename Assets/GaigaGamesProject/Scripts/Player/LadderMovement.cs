using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    private float vertical;
    [SerializeField] private float speed = 2.5f;
    private bool isLadder;
    private bool isClimbing;
    private float defaultGravity;

    [SerializeField] private Rigidbody2D playerBody;



    private void Start()
    {
        defaultGravity = playerBody.gravityScale;

        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.MainGame.MoveVertical.performed += ctx =>
        {
            vertical = ctx.ReadValue<float>() * speed;
        };
    }

    // Update is called once per frame
    void Update()
    {
        //vertical = Input.GetAxis("Vertical");
        //Debug.Log(vertical);

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            playerBody.gravityScale = 0f;
            playerBody.velocity = new Vector2(playerBody.velocity.x, vertical);
        }
        else
        {
            playerBody.gravityScale = defaultGravity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            Debug.Log("Is in ladder");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
