using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    Vector2 moveInput;

    public float CurrentMoveSpeed { get
        {
            if (IsMoving)
            {
                if(IsRunning)
                {
                    return runSpeed;
                }else
                {
                    return walkSpeed;
                }
            }
            else
            {
                //Idle speed is = 0
                return 0;
            }
        } }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving { get 
        {
            return _isMoving;
        } 
        private set 
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning 
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight { get { return _isFacingRight; } private set
        {
            if(_isFacingRight != value)
            {
                //Flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            
            _isFacingRight = value;

        }
    }

    Rigidbody2D rb;
    Animator animator;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  // This line will check and get this component if available in the gameobject Player
        animator = GetComponent<Animator>(); // This line will check and get this component if available in the gameobject Player
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);

    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            // Face the right
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            //Face the left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }else if (context.canceled)
        {
            IsRunning = false;
        }

    }
}
