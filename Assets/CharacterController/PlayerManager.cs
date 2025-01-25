using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody playerRigidBody;
    public float moveSpeed = 5.0f;
    public float delay = 1.0f;
    private Vector3 moveVector = Vector3.zero;
    public float sprintMod = 1.0f;
    public Vector3 jumpHeight = new Vector3 (0, 2f, 0 );
    public float jumpForce = 2.0f;
    private bool onGround;
    private bool isSprinting;



    void Start()
    {
        Actions.movement += updateMoveVector;
        Actions.jump += jump;
        Actions.sprintStart += sprintStart;
        Actions.sprintEnd += sprintEnd;
    }

    private void FixedUpdate()
    {
        moveChar(moveVector);
    }
    void moveChar(Vector3 moveVector)
    {
        playerRigidBody.MovePosition(playerRigidBody.position + (moveVector * moveSpeed * Time.fixedDeltaTime) * sprintMod);
    }
    private void OnCollisionEnter()
    {
        onGround = true;
    }
    private void OnCollisionExit()
    {
        onGround = false;
    }
    void jump()
    {
        if (onGround)
        {
            playerRigidBody.AddForce(jumpHeight * jumpForce, ForceMode.Impulse);
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
            sprintMod = 2.5f;
        }
    }
    void sprintStart()
    {
        isSprinting = true;
        if (onGround)
        {
            sprintMod = 2.5f;
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
