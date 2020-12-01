
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

    private bool IsGrounded = true;
    private Rigidbody Rb;
    private Vector2 InputValue;
    private Vector3 JumpHeight;

    private BoxCollider SpinCollider;
    private bool IsSpinning;

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

    //This function handles the movement of the character and which way it must face according to the player's input.
    private void Direction()
    {
        //Gets the direction of the players input
        Vector3 direction = new Vector3(InputValue.x, 0, InputValue.y);

        Vector3 velocity = direction * Speed;
        Rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        Vector3 facingrotation = Vector3.Normalize(new Vector3(InputValue.x, 0, InputValue.y));

        //This condition prevents from spamming "Look rotation viewing vector is zero" when not moving.
        if (facingrotation != Vector3.zero)
        {
            transform.forward = facingrotation;
        }
    }

    //Once the player collides with something the Boolean IsGrounded will be set to true so the player can jump again.
    private void OnCollisionEnter(Collision other)
    {
        IsGrounded = true;
    }

    //This function checks if the player is grounded, if so that means the player is not in the air and can jump.
    //Once the player jumps the boolean IsGrounded will be set to false.
    public void Jumping()
    {
        if (IsGrounded)
        {
            Rb.AddForce(JumpHeight * JumpForce, ForceMode.Impulse);
            IsGrounded = false;
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
        Rb.velocity = new Vector3(Rb.velocity.x, 0);
        Rb.AddForce(new Vector3(0, 400));
    }

    private void OnJump()
    {
        Jumping();
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
}
