
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public float JumpHeightFloat;
    public float Bounce;
    public float GroundCheckRadius;
    public float FallingCutoff;
    public LayerMask GroundMask;
    public Transform GroundChecker;

    private bool IsGrounded;
    private bool HasDoubleJumped;
    private bool HoldingJump;
    private Rigidbody Rb;
    private Vector2 InputValue;
    private Vector3 JumpHeight;
    private BoxCollider SpinCollider;
    private bool IsSpinning;
    private float JumpingCooldown = .1f;
    private float CurrentJumpingCooldown;
    private float FallingMovement;

    //Gets the Rigidbody of the player, the box collider of the spin attack and sets the jumpheight of the player.
    private void Awake()
    {
        SpinCollider = GetComponent<BoxCollider>();
        Rb = GetComponent<Rigidbody>();
        JumpHeight = new Vector3(0.0f, JumpHeightFloat, 0.0f);
    }

    //Keeps updating the direction everytime the player gives an input.
    void FixedUpdate()
    {
        Direction();
    }

    void Update()
    {
        IsGrounded = Physics.CheckSphere(GroundChecker.position, GroundCheckRadius, GroundMask);

        if (CurrentJumpingCooldown >= 0)
        {
            CurrentJumpingCooldown -= Time.deltaTime;
        }
        if (IsGrounded)
        {
            FallingMovement = 1; 
        }
        else
        {
            if(FallingMovement >= 0)
            {
                FallingMovement -= FallingCutoff * Time.deltaTime;
            }          
        }
    }

    private void ResetJumpingCooldown()
    {
        CurrentJumpingCooldown = JumpingCooldown;
    }

    //This function handles the movement of the character and which way it must face according to the player's input.
    private void Direction()
    {
        //Gets the direction of the players input
        Vector3 direction = new Vector3(InputValue.x, 0, InputValue.y);

        Vector3 velocity = direction * Speed * FallingMovement;
        Rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        Vector3 facingrotation = Vector3.Normalize(new Vector3(InputValue.x, 0, InputValue.y));

        //This condition prevents from spamming "Look rotation viewing vector is zero" when not moving.
        if (facingrotation != Vector3.zero)
        {
            transform.forward = facingrotation;
        }
    }

    //This function checks if the player is grounded, if so that means the player is not in the air and can jump.
    public void Jumping()
    {
        if (CurrentJumpingCooldown > 0)
        {
            return;
        }
        if (IsGrounded || !HasDoubleJumped)
        {
            if(Rb.velocity.y < 0)
            {
                Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z);
            }
            if (IsGrounded)
            {
                HasDoubleJumped = false;
            }
            else
            {
                HasDoubleJumped = true;
            }
            Rb.AddForce(JumpHeight * JumpForce, ForceMode.Impulse);
            ResetJumpingCooldown();
            FallingMovement = 1;
        }
    }
    //This function gives a downwards momentem when the player hits something from below.
    public void BounceDown()
    {
        Rb.velocity = new Vector3(Rb.velocity.x, 0);
        Rb.AddForce(new Vector3(0,-400));
    }

    public void BounceUp()
    {
        float JumpHeight = 400f;

        if (HoldingJump)
        {
            JumpHeight = 475f;
            ResetJumpingCooldown();
        }
        Rb.velocity = new Vector3(Rb.velocity.x, 0);
        Rb.AddForce(new Vector3(0, JumpHeight));
        HasDoubleJumped = false;
        FallingMovement = 1;
    }

    private void OnJumpPressed()
    {
        HoldingJump = true;
        Jumping();
    }

    private void OnJumpReleased()
    {
        HoldingJump = false;
    }

    private void OnMove(InputValue val)
    {
        InputValue = val.Get<Vector2>();
    }

    //Once the player presses the attack button the Coroutine SpinAttack will start.
    private void OnSpin()
    {
        StartCoroutine(SpinAttack());
    }

    IEnumerator SpinAttack()
    {
        if (!IsSpinning)
        {
            IsSpinning = true;
            SpinCollider.enabled = true;
            yield return new WaitForSeconds(1);
            SpinCollider.enabled = false;
            IsSpinning = false;
        }
    }

   /* Testing purposes only. Remove at final build!!!
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundChecker.position, GroundCheckRadius);
    }
    */
}
