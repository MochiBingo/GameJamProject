using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, ActionsPlayer.IPlayerInputActions
{
    private ActionsPlayer playerInput;

    void Awake()
    {
        playerInput = new ActionsPlayer();
        playerInput.PlayerInput.Enable();
        playerInput.PlayerInput.SetCallbacks(this);
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        Actions.movement?.Invoke(context.ReadValue<Vector3>());
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Actions.jump?.Invoke();
        }
    }
    public void OnDashDive(InputAction.CallbackContext context)
    {
        Debug.Log("a");
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("a");
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        Debug.Log("a");
    }
}
public static class Actions
{
    public static Action<Vector3> movement;
    public static Action jump;
}
