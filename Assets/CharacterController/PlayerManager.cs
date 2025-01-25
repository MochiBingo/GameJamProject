using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody playerRigidBody;
    public float moveSpeed = 5.0f;
    public float delay = 1.0f;
    private Vector3 moveVector = Vector3.zero;
    public float sprintMod = 10.0f;
    public Vector3 jumpHeight = new Vector3 (0, 3f, 0 );
    public float jumpForce = 3.0f;
    private bool onGround;

    private void OnCollisionStay()
    {
        onGround = true;
    }

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
        playerRigidBody.MovePosition(playerRigidBody.position + moveVector * moveSpeed * Time.fixedDeltaTime);
    }
    void jump()
    {
        if (onGround)
        {
            playerRigidBody.AddForce(jumpHeight * jumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }
    void sprintStart()
    {
        
    }
    void sprintEnd()
    {
       
    }
    void updateMoveVector(Vector3 inputVector)
    {
        moveVector = inputVector;
    }
}
