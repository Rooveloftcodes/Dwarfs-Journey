using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

/*[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]*/
public class PlayerController : MonoBehaviour
{
    /*Vector2 moveInput;

    [SerializeField] private bool _isMoving = false;
    [SerializeField] public float walkSpeed = 1.5f;
    public bool _isFacingRight = true;
    public float jumpImpulse = 10;    

    TouchingDirections touchingDirections;
    Rigidbody2D rb;
    Animator animator;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool isFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    //deleted start and update methods
    public void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }
    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = false;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        //TODO check if alive
        if(context.started && touchingDirections.IsGrounded)
        {
            Debug.Log("Jump key pressed");
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }*/
    [SerializeField] public float speed;
    private Rigidbody2D body;
    private Animator anim;

    private float horizontalInput;
    private bool isGrounded;
    private object isJumping;

    private void Awake()
    {
        //get references for components
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        if(Input.GetKey(KeyCode.Space) && isGrounded)
        {
            //TODO increase jump height
            jump();
        }
        anim.SetBool("isRunning", horizontalInput != 0);
        anim.SetBool("isGrounded", isGrounded);
    }
    void jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("isJumping");
        anim.SetBool("isRunning", false);
        isGrounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)//player is colliding with tilemap/ground
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}