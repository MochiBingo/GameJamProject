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
    public float sprintMod = 1.0f;

    void Start()
    {
        Actions.movement += updateMoveVector;
        Actions.jump += jump;
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
        playerRigidBody.MovePosition(playerRigidBody.position + new Vector3(0, 1.5f, 0));
    }
    void updateMoveVector(Vector3 inputVector)
    {
        moveVector = inputVector;
    }
}
