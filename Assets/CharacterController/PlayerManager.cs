using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody playerRigidBody;

    public float moveSpeed = 5.0f;
    public float baseSpeed = 5.0f;
    public float delay = 1.0f;

    private Vector3 moveVector = Vector3.zero;
    public float sprintMod = 1.0f;
    public Vector3 jumpHeight = new Vector3(0, 2f, 0);
    public float dashSpeed = 15f;
    public float jumpForce = 2.0f;
    private bool onGround;
    private bool isSprinting;
    private HealthSystem healthSystem;
    private bool canJump = true;
    public GameObject loseTextObject;
    private PlayerAnimationController animationController;
    private bool isMoving;
    private bool isRunning;
    private Camera mainCamera;
    private Transform playerTransform;

    void Start()
    {
        mainCamera = Camera.main;
        playerTransform = transform;
        Cursor.lockState = CursorLockMode.Locked;
        healthSystem = gameObject.GetComponent<HealthSystem>();

        animationController = GetComponent<PlayerAnimationController>();
    }
    private void OnEnable()
    {
        Actions.movement += updateMoveVector;
        Actions.jump += jump;
        Actions.sprintStart += sprintStart;
        Actions.sprintEnd += sprintEnd;
        Actions.dash += dash;
    }
    private void OnDisable()
    {
        Actions.movement -= updateMoveVector;
        Actions.jump -= jump;
        Actions.sprintEnd -= sprintEnd;
        Actions.dash -= dash;
        Destroy(this);
    }
    private void FixedUpdate()
    {
        if (healthSystem.isDead == true)
        {
            playerRigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            canJump = false;
        }
        moveChar(moveVector);
    }
    void moveChar(Vector3 moveVector)
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = cameraForward * moveVector.z + cameraRight * moveVector.x;
        playerRigidBody.MovePosition(playerRigidBody.position + (moveDirection * moveSpeed * Time.fixedDeltaTime) * sprintMod);
    }
    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // Check if the contact normal is pointing upward (we're touching ground)
            if (contact.normal.y > 0.7f)
            {
                onGround = true;
                if (isDashing)
                {
                    // Only zero out horizontal velocity (x and z)
                    playerRigidBody.velocity = new Vector3(0, playerRigidBody.velocity.y, 0);
                    isDashing = false;
                }
                if (isSprinting)
                {
                    isRunning = true;
                    animationController.SetRunning(true);
                    sprintMod = 1.75f;
                }
                else
                {
                    // Check if we should be walking based on current movement
                    if (moveVector.magnitude > 0)
                    {
                        animationController.SetWalking(true);
                        animationController.SetRunning(false);
                    }
                    else
                    {
                        animationController.SetWalking(false);
                        animationController.SetRunning(false);
                    }
                }
                // Reset dash ability when landing
                canDash = true;
                return;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"collided with {other.name}");
        if (other.CompareTag("Enemy"))
        {
            healthSystem.takeDamage(1);
        }
    }
    void jump()
    {
        if (onGround && canJump)
        {
            animationController.TriggerJump();
            playerRigidBody.AddForce(jumpHeight * jumpForce, ForceMode.Impulse);
        }
    }
    private bool isDashing = false;
    private bool canDash = true;

    void dash()
    {
        // Only allow dashing while in the air and if we haven't used our dash
        if (!onGround && canDash)
        {
            animationController.TriggerDash();
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();
            
            // Set a fixed velocity for the dash
            Vector3 dashVelocity = cameraForward * dashSpeed;
            // Keep current vertical velocity
            dashVelocity.y = playerRigidBody.velocity.y;
            
            // Apply the dash velocity
            playerRigidBody.velocity = dashVelocity;
            
            isDashing = true;
            canDash = false;
        }
    }


    private void Update()
    {

        // Calculate sprint modifier
        if (onGround && !isSprinting)
        {
            sprintMod = 1.0f;
        }
        if (onGround && isSprinting)
        {
            sprintMod = 1.75f;
        }
        
        // Apply movement speed
        moveSpeed = baseSpeed * sprintMod;


        if (!isDashing)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0;
            cameraForward = cameraForward.normalized;
            if (cameraForward != Vector3.zero && moveVector.magnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
                playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

    }
    void sprintStart()
    {
        isSprinting = true;
        if (onGround)
        {
            isRunning = true;
            animationController.SetRunning(true);
            sprintMod = 1.75f;
        }    
    }
    void sprintEnd()
    {
        isSprinting = false;
        if (onGround)
        {
            isRunning = false;
            animationController.SetRunning(false);
            sprintMod = 1.0f;
        }
    }
    void updateMoveVector(Vector3 inputVector)
    {
        isMoving = inputVector.magnitude > 0;
        animationController.SetWalking(isMoving && !isRunning);
        animationController.SetRunning(isMoving && isRunning);
        moveVector = inputVector;
    }
}
