
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
    public float DoubleJumpCheckRadius;
    public float FallingCutoff;
    public LayerMask GroundMask;
    public LayerMask DoubleJumpMask;
    public Transform GroundChecker;
    [HideInInspector]
    public bool CanMove = true;
    public GameEvent UpdateUI;
    public BoxCollider SpinCollider;
    public Animator HUDObjects;
    public Animator HUDText;

    private Animation AnimSpinAttack;
    private Animator PlayerAnimator;
    private bool IsGrounded;
    private bool HasDoubleJumped;
    private bool HoldingJump;
    private Rigidbody Rb;
    private Vector2 InputValue;
    private Vector3 JumpHeight;
    private bool IsSpinning;
    private float JumpingCooldown = .1f;
    private float CurrentJumpingCooldown;
    private float FallingMovement;
    private Transform CheckPoint;
    private Vector3 OriginPosition;
    private PlayerStatus PlayerStatus;

    private void Awake()
    {
        PlayerAnimator = GetComponentInChildren<Animator>();
        SpinCollider = GetComponent<BoxCollider>();
        AnimSpinAttack = GetComponentInChildren<Animation>();
        AnimSpinAttack.gameObject.SetActive(false);
        PlayerStatus = GetComponent<PlayerStatus>();
        OriginPosition = gameObject.transform.position;
        Rb = GetComponent<Rigidbody>();
        JumpHeight = new Vector3(0.0f, JumpHeightFloat, 0.0f);
    }

    //Checks if the bool CanMove is true, if so it keeps updating the direction everytime the player gives an input.
    void FixedUpdate()
    {
        if (CanMove)
        {
            Direction();
        }       
    }

    //Checks if the player is colliding with an object or is still falling down.
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
            if (FallingMovement >= 0)
            {
                FallingMovement -= FallingCutoff * Time.deltaTime;
            }
        }
    }

    //Resets the jumpingcooldown so the player is able to perform a double jump again.
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

    //This function lets the player either jump or double jump depending if the player is allowed to or not.
    public void Jumping()
    {
        bool DoubleJumpCollision = Physics.CheckSphere(GroundChecker.position, DoubleJumpCheckRadius, DoubleJumpMask);

        if (CurrentJumpingCooldown > 0)
        {
            return;
        }
        if (IsGrounded || (!HasDoubleJumped && !DoubleJumpCollision))
        {
            if(Rb.velocity.y < 0)
            {
                Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z);
            }
            if (IsGrounded)
            {
                Rb.velocity = new Vector3(0, 0, 0);
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

    //This function gets called when the player is jumping on an object that you bounce up from.
    //Depending if the player is holding the jump button the players height will vary.
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

    //Once the player presses the jump button HoldingJump will be set to true and the Jumping function will be called.
    private void OnJumpPressed()
    {
        HoldingJump = true;
        Jumping();
    }
    //Once the player isn't pressing the jump button anymore HoldingJump will be set to false.
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

    //Once this function gets called it sets the Transform CheckPoint to the NewCheckpoint transform.
    public void SaveCheckpoint(Transform NewCheckpoint)
    {
        CheckPoint = NewCheckpoint;
    }
    
    //Once this function gets called it displays the HUD
    private void OnHUD()
    {
        StartCoroutine(DisplayHUD());
    }

    //This function gets called when the player died and needs to be respawned.
    public void SpawnPlayer()
    {
        //Checks if the player has already hit a checkpoint, if so it resets the player to the checkpoint position. If not it resets the player to the beginning of the level.
        if (CheckPoint != null)
        {
            gameObject.transform.position = CheckPoint.position;
        }             
        else
        {
            gameObject.transform.position = OriginPosition;
        }
    }

    public void AddWoompa()
    {
        PlayerStatus.Player.Woompa++;
        if (PlayerStatus.Player.Woompa >= 99)
        {
            AddExtraLife();
            PlayerStatus.Player.Woompa = 0;
        }
        UpdateUI.Raise();

    }

    public void AddExtraLife()
    {
        PlayerStatus.Player.Lives++;
        UpdateUI.Raise();
    }

    IEnumerator SpinAttack()
    {
        if (!IsSpinning)
        {
            IsSpinning = true;
            PlayerAnimator.gameObject.SetActive(false);
            AnimSpinAttack.gameObject.SetActive(true);
            SpinCollider.GetComponent<BoxCollider>().enabled = true;           
            yield return new WaitForSeconds(0.5f);
            SpinCollider.GetComponent<BoxCollider>().enabled = false;
            PlayerAnimator.gameObject.SetActive(true);
            AnimSpinAttack.gameObject.SetActive(false);
            IsSpinning = false;
        }
    }

    IEnumerator DisplayHUD()
    {
        HUDObjects.SetBool("Display", true);
        HUDText.SetBool("Display", true);
        yield return new WaitForSeconds(5);
        HUDObjects.SetBool("Display", false);
        HUDText.SetBool("Display", false);
    }

    /*//Testing purposes only. Remove at final build!!!
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundChecker.position, GroundCheckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GroundChecker.position, DoubleJumpCheckRadius);
    }
    */
}
