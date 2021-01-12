
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public float MovementSpeed;
    public float TurningSpeed;
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
    public Animator UILivesObject;
    public Animator UILivesText;
    public Animator UIWoompaObject;
    public Animator UIWoompaText;

    private bool GotWoompa = false;
    private bool GotLife = false;
    private bool DisplayHud = false;
    [HideInInspector]
    public bool StandingIdle = true;
    [HideInInspector]
    public float StartIdle = 0f;

    private float StartIdle2 = 15f;
    float TimerHUD = 0f;
    [HideInInspector]
    public float TimerWoompa = 0f;
    [HideInInspector]
    public float TimerLife = 0f;
    [HideInInspector]
    public Animator PlayerAnimator;

    private Animation AnimSpinAttack;
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

    public AudioSource AkuAkuWithDrawSource;
    public AudioSource ExtraLifeSource;
    public AudioSource WoompaSource;

    //[HideInInspector]
    public bool CanHit = true;
    public bool NotPlaying = true;
    private float Invulnerable = 3;
    private float Blink = 1f;
    private Renderer[] Rend;

    private void Awake()
    {
        Rend = GetComponentsInChildren<Renderer>();
        PlayerAnimator = GetComponentInChildren<Animator>();
        SpinCollider = GetComponent<BoxCollider>();
        AnimSpinAttack = GetComponentInChildren<Animation>();
        PlayerStatus = GetComponent<PlayerStatus>();
        AnimSpinAttack.gameObject.SetActive(false);
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

    void Update()
    {
        StartIdle += Time.deltaTime;
        if(StandingIdle)
        {
            if (StartIdle > StartIdle2)
            {
                PlayerAnimator.ResetTrigger("LongIdle");
                PlayerAnimator.SetTrigger("LongIdle");
                StandingIdle = false;
            }
        }

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

        if (DisplayHud)
        {
            TimerLife = 5f;
            TimerWoompa = 5f;
            TimerHUD = 5f;
        }
        if (TimerWoompa > 0)
        {
            TimerWoompa -= Time.deltaTime;
            ShowWoompa();
        }
        else
        {
            HideWoompa();
        }
        if (TimerHUD > 0)
        {
            TimerHUD -= Time.deltaTime;
            ShowHUD();
        }
        else
        {
            HideHUD();
        }
        if (TimerLife > 0)
        {
            TimerLife -= Time.deltaTime;
            ShowLives();
        }
        else
        {
            HideLives();
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

        Vector3 velocity = direction * MovementSpeed * FallingMovement;
        Rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        Vector3 facingrotation = Vector3.Normalize(new Vector3(InputValue.x, 0, InputValue.y));

        //This condition prevents from spamming "Look rotation viewing vector is zero" when not moving.
        if (facingrotation != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, facingrotation, TurningSpeed * Time.deltaTime);
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
        DisplayHud = true;
    }

    //Once this function gets called it displays the HUD
    private void OnHUDReleased()
    {
        DisplayHud = false;
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

    //This function adds Woompa fruit to the playerstatus SO, raises the UpdateUI event and if the player has over 99 Woompa fruit it resets the amount to 0 and calls the function AddExtraLife.
    public void AddWoompa()
    {
        WoompaSource.Play();
        TimerWoompa = 5f;
        PlayerStatus.Player.Woompa++;
        UpdateUI.Raise();
        if (PlayerStatus.Player.Woompa >= 99)
        {
            PlayerStatus.Player.Woompa = 0;
            AddExtraLife();           
        }           
    }

    //This function sets the TimerLife to 5, adds an extra life to the playerstatus and raises the UpdateUI event.
    public void AddExtraLife()
    {
        ExtraLifeSource.Play();
        TimerLife = 5;
        PlayerStatus.Player.Lives++;
        UpdateUI.Raise();
    }

    private void HideHUD()
    {
        HUDObjects.SetBool("DisplayAll", false);
        HUDText.SetBool("DisplayAll", false);
    }

    private void ShowHUD()
    {
        HUDObjects.SetBool("DisplayAll", true);
        HUDText.SetBool("DisplayAll", true);
    }

    private void HideLives()
    {
        UILivesObject.SetBool("Display", false);
        UILivesText.SetBool("Display", false);
    }

    private void ShowLives()
    {
        UILivesObject.SetBool("Display", true);
        UILivesText.SetBool("Display", true);
    }
    
    private void HideWoompa()
    {
        UIWoompaObject.SetBool("Display", false);
        UIWoompaText.SetBool("Display", false);
    }

    private void ShowWoompa()
    {
        UIWoompaObject.SetBool("Display", true);
        UIWoompaText.SetBool("Display", true);
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

    //This coroutine Displays the player being hit and is temporarely invulnerability to other enemies to give the player a fair change to save himself for a short period of time.
    public IEnumerator TempInvulnerability()
    {
        if (NotPlaying)
        {
            NotPlaying = false;
            while (Invulnerable > 0)
            {
                Invulnerable -= Time.deltaTime;
                foreach (Renderer rend in Rend)
                {
                    rend.enabled = !rend.enabled;
                    new WaitForSeconds(Blink);
                }
                yield return null;
            }           
            foreach (Renderer rend in Rend)
            {
                rend.enabled = true;
            }
            CanHit = true;
            NotPlaying = true;
            Invulnerable = 3;
            yield return null;
        }     
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
