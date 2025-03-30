using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Vector2 moveInput;
    public Rigidbody2D playerRb;
    public float jumpForce = 5f;
    public float speed = 2f;
    public float speedRun = 4f;
    public float speedAdd;
    public bool facingRight = true;
    public bool isAttacking = false;
    public int attackIndex = 0;
    private int maxAttackIndex = 3;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask layerGround;

    [SerializeField] 
    private InputActionReference movement;
    private IPlayerState currentState;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        ChangeState(new IdleState());
        speedAdd = speed;
    }

    void Update()
    {
        moveInput = movement.action.ReadValue<Vector2>();
        currentState.UpdateState(this);
    }


    public void Move()
    {
        playerRb.linearVelocity = new Vector2(moveInput.x * speedAdd, playerRb.linearVelocity.y);
    }

    public void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.01f, layerGround);
    }

    public void Jump(InputAction.CallbackContext context)
    {
       if(context.performed && IsGrounded() && !isAttacking)
        {
            ChangeState(new JumpState());
        }
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && !isAttacking)
        {
            if(moveInput != Vector2.zero)
            {
                speedAdd = speedRun;    
                if(!(currentState is SprintState))
                {
                    ChangeState(new SprintState()); 

                }
            }
            else
            {
                speedAdd = speed;
            }
        }
        else if(context.canceled)
        {
            speedAdd = speed;
            if (currentState is SprintState)
            {
                ChangeState(new MoveState());
            }

        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking && IsGrounded())
        {
            isAttacking = true;
            ChangeState(new AttackState(attackIndex));
            attackIndex = (attackIndex + 1) % maxAttackIndex;
        }
    }

    public void OnAttackEnd()
    {
        isAttacking = false;
        ChangeState(new IdleState());
    }   

    public void ChangeState(IPlayerState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }
        currentState = newState;
        newState.EnterState(this);
    }
}
