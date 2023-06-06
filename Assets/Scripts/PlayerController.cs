using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private bool _isMoving = false;
    public float walkSpeed = 5f;    
    Vector2 moveInput;    
    Rigidbody2D rb;

    Animator animator;

    public bool IsMoving 
    {
        get {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }
    void Update()
    {
         
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed * Time.fixedDeltaTime, rb.velocity.y); 
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }
}
