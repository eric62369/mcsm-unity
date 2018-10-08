using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    /// <summary>
    /// The Maximum speed player can travel horizontally
    /// </summary>
    public float maxHorizontalSpeed;

    /// <summary>
    /// The force applied by a jump
    /// </summary>
    public float JumpForce;

    // Real moving speed
    private float horizontalVelocity;

    /// <summary>
    /// Is the player on the ground?
    /// </summary>
    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool IsFalling;

    public Animator animator;
    //public float DashTime;
    //private float dashtimer;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        animator.SetBool("IsGrounded", isGrounded);
        IsFalling = (GetComponent<Rigidbody2D>().velocity.y < -0.1); // TODO: Kinda a bad solution
        animator.SetBool("IsFalling", IsFalling);
    }

    // Update is called once per frame
    void Update () {
        horizontalVelocity = 0.0f;
        // Right
        if (Input.GetKey(KeyCode.D))
        {
            //GetComponent<Rigidbody2D>().AddForce(Vector2.right * maxHorizontalSpeed, ForceMode2D.Force);
            horizontalVelocity = maxHorizontalSpeed;
            UpdateLocalScaleOrientation();
        }
        // Left
        else if (Input.GetKey(KeyCode.A))
        {
            //GetComponent<Rigidbody2D>().AddForce(Vector2.left * maxHorizontalSpeed, ForceMode2D.Force);
            horizontalVelocity = -maxHorizontalSpeed;
            UpdateLocalScaleOrientation();
        }
        //else
        //{
        //    GetComponent<Rigidbody2D >
        //}

        setHorizontalVelocity(horizontalVelocity);
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            ApplyJumpForce();
        }
	}

    private void UpdateLocalScaleOrientation()
    {
        if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            GetComponent<Transform>().localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void setHorizontalVelocity(float horizontalVelocity)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(x: horizontalVelocity, y: GetComponent<Rigidbody2D>().velocity.y);
        animator.SetFloat("MoveSpeed", Math.Abs(horizontalVelocity));
    }

    /// <summary>
    /// Make the player jump
    /// </summary>
    private void ApplyJumpForce()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce, ForceMode2D.Force);
    }
}
