using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody Rb;
    private Vector2 inputValue;
    private 
    private bool IsGrounded;
    public float Speed;
    public float JumpHeight;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Direction();
    }

    private void Direction()
    {
        //Gets the direction of the players input
        Vector3 direction = new Vector3(inputValue.x, 0, inputValue.y);

        Vector3 velocity = direction * Speed;
        Rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        Vector3 facingrotation = Vector3.Normalize(new Vector3(inputValue.x, 0, inputValue.y));

        //This condition prevents from spamming "Look rotation viewing vector is zero" when not moving.
        if (facingrotation != Vector3.zero)
        {
            transform.forward = facingrotation;
        }

        if (IsGrounded)
        {
            Rb.AddForce(new Vector3(0, JumpHeight, 0), ForceMode.Impulse);
        }
    }

    private void OnMove(InputValue val)
    {
        inputValue = val.Get<Vector2>();
    }
}
