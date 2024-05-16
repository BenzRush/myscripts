using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Animator playerAnim;
    public float groundDrag;
    bool isKicking;

    [Header("Player Stats")]
    public int health;
    public int DamageReceived;
    public Healthbar healthbar;
    public int damage;
    private int currentHealth;
    private bool canCombo;

    [Header("Jump Settings")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public string HorizontalAxis;
    public string VerticalAxis;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode KickKey = KeyCode.E;
    public KeyCode AirAttack = KeyCode.Q;
    public KeyCode TakeDamage = KeyCode.T;
    public KeyCode SpecialAttack = KeyCode.F;
    public KeyCode DownCombo = KeyCode.S;
    public KeyCode UpCombo = KeyCode.W;
    public KeyCode Blocking = KeyCode.R;
    public KeyCode Spawn = KeyCode.G;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    private bool isAttacking = false;

    [Header("Attack Cooldowns")]
    public float KickCooldown = 1f;
    public float BlockCooldown = 1f;
    public float AirAttackCooldown = 1f;
    public float SpecialAttackCooldown = 1f;
    public float DownComboCooldown = 1f;
    public float UpComboCooldown = 4f;

    [Header("Other Settings")]
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    private InputDevice inputDevice;


    public float forceMovementSpeed = 10f;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        canCombo = false;
        PlayerStats();
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        myInput();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;



    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void SetInputDevice(InputDevice device)
    {
        inputDevice = device;
    }
    public void myInput()
    {

        if (inputDevice is Gamepad)
        {
            // Handle gamepad-specific input
        }
        else if (inputDevice is Keyboard)
        {
            // Handle keyboard-specific input
        }

        horizontalInput = Input.GetAxisRaw(HorizontalAxis);
        verticalInput = Input.GetAxisRaw(VerticalAxis);

        if (Input.GetKeyDown(TakeDamage))
        {
            Damage(DamageReceived);
        }

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKey(KickKey) && !isAttacking)
        {
            PlayerKick();
        }

        if (Input.GetKey(Blocking) && !isAttacking)
        {
            PlayerBlock();
        }

        if (Input.GetKey(AirAttack) && canCombo == false && !isAttacking)
        {
            PlayerAirAttack();
         
        }

        if (Input.GetKey(AirAttack) && Input.GetKey(DownCombo) && canCombo == true && !isAttacking)
        {
            PlayerCombo1();
        }

        if (Input.GetKey(SpecialAttack) && canCombo == false && !isAttacking)
        {
            PlayerSpecialAttack();
         
        }

        if (Input.GetKey(SpecialAttack) && Input.GetKey(UpCombo) && canCombo == true && !isAttacking && canCombo)
        {
            PlayerCombo2();
        }
        if(Input.GetKeyDown(UpCombo)||Input.GetKeyDown(DownCombo)) 
        { 
         canCombo = true;
        }
        if (Input.GetKeyUp(UpCombo) || Input.GetKeyUp(DownCombo))
        {
            canCombo = false;
        }
    }

    public void MovePlayer()
    {

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * forceMovementSpeed, ForceMode.Force);


        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * forceMovementSpeed * airMultiplier, ForceMode.Force);

    }


    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        playerAnim.Play("isJumping");
    }

    public void ResetJump()
    {
        readyToJump = true;
    }

    public void PlayerKick()
    {
        isAttacking = true;
        playerAnim.Play("isKicking");
        StartCoroutine(ResetAttackFlagAfterDelay(KickCooldown));
    }

    public void PlayerBlock()
    {
        isAttacking = true;
        playerAnim.Play("isBlocking");
        StartCoroutine(ResetAttackFlagAfterDelay(BlockCooldown));
    }

    public void PlayerAirAttack()
    {
        isAttacking = true;
        playerAnim.Play("isFlyingAttack");
        StartCoroutine(ResetAttackFlagAfterDelay(AirAttackCooldown));
        
    }

    public void PlayerSpecialAttack()
    {
        isAttacking = true;
        playerAnim.Play("isSpecialAttacking");
        StartCoroutine(ResetAttackFlagAfterDelay(SpecialAttackCooldown));
        
    }

    public void PlayerCombo1()
    {
        isAttacking = true;
        canCombo = true;
        playerAnim.Play("isCombo1");
        StartCoroutine(ResetAttackFlagAfterDelay(DownComboCooldown));
        StartCoroutine(ResetComboFlagAfterDelay(1f));
    }
    public void PlayerCombo2()
    {
        isAttacking = true;
        canCombo = true;
        playerAnim.Play("isCombo2");
        StartCoroutine(ResetAttackFlagAfterDelay(UpComboCooldown));
        StartCoroutine(ResetComboFlagAfterDelay(1f));
    }

    private IEnumerator ResetComboFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canCombo = false;
    }

    private IEnumerator ResetAttackFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
    }

    public void PlayerStats()
    {
        currentHealth = health;
        healthbar.SetMaxHealth(health);
    }

    public void Damage(int damage)
    {
        health = health - damage;
        healthbar.SetHealth(health);
        if (health <= 0)
        {
            Debug.Log("Player has died.");
            health = 0;
            playerAnim.Play("isDeath");


        }
    }
//    public void SetPlayerNumber(int playerNumber)
//    {
//        Debug.Log("Player " + playerNumber + " spawned!");

//        Spawn = (playerNumber == 1) ? KeyCode.Return : KeyCode.Return;
//    }
}


