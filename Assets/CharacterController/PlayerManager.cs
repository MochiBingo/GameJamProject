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


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        healthSystem = gameObject.GetComponent<HealthSystem>();
        Actions.movement += updateMoveVector;
        Actions.jump += jump;
        Actions.sprintStart += sprintStart;
        Actions.sprintEnd += sprintEnd;
        Actions.dash += dash;
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
    void OnCollisionEnter()
    {
        onGround = true;
        if (isDashing)
        {
            // Only zero out horizontal velocity (x and z)
            playerRigidBody.velocity = new Vector3(0, playerRigidBody.velocity.y, 0);
            isDashing = false;
        }
        // Reset dash ability when landing
        canDash = true;
    }
    void OnCollisionExit()
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
        if (onGround && !isSprinting)
        {
            sprintMod = 1.0f;
        }
        if (onGround && isSprinting)
        {
            sprintMod = 1.75f;
        }
    }
    void sprintStart()
    {
        isSprinting = true;
        if (onGround)
        {
            sprintMod = 1.75f;
        }    
    }
    void sprintEnd()
    {
        isSprinting = false;
        if (onGround)
        {
            sprintMod = 1.0f;
        }
    }
    void updateMoveVector(Vector3 inputVector)
    {
        moveVector = inputVector;
    }
}
