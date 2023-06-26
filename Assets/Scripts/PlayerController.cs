using UnityEngine;
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
    [Header("Physics")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Controls")]
    private float attackCoolDown = 1.5f;

    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator animator;    

    private float horizontalInput;
    private float coolDownTimer = Mathf.Infinity;
    public bool isGrounded;

    protected ExtraScript extrascript;

    private void Awake()
    {
        //get references for components
        animator = GetComponent<Animator>();   
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();         
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        if(horizontalInput > 0f)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < 0f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        if(Input.GetKey(KeyCode.Space) && Grounded())
        {
            jump();
        }
        if (Input.GetKey(KeyCode.X) && canAttack())
        {
            //arrow shoots here
            Debug.Log("Attacking");
            timerReset();
        }
        //if(OnCollisionEnter2D())
        animator.SetBool("isRunning", horizontalInput != 0);
        animator.SetBool("isGrounded", isGrounded);
    }
    private void FixedUpdate()
    {
        coolDownTimer += Time.deltaTime;
        /*if(coolDownTimer > attackCoolDown)
        {
            Debug.Log("coolDownTimer > attackCoolDown");
        }*/
    }
    public void jump()
    {
        //buggy again
        body.velocity = new Vector2(body.velocity.x, speed * jumpForce);
        animator.SetTrigger("isJumping");
        animator.SetBool("isRunning", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)//player is colliding with tilemap/ground
    {
        if (collision.gameObject.tag == "Ground") isGrounded = true; else isGrounded = false;
    }
    private bool Grounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        if (coolDownTimer > attackCoolDown && isGrounded) return true; else return false;
        //return horizontalInput == 0 && Grounded();
    }
    void timerReset()
    {
        coolDownTimer = 0f;
    }
}