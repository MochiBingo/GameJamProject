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
        if (context.performed)
        {
            Actions.dash?.Invoke();
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Actions.interact?.Invoke();
        }    
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Actions.sprintStart?.Invoke();
        }
        if (context.canceled)
        {
            Actions.sprintEnd?.Invoke();
        }
    }
}
public static class Actions
{
    public static Action<Vector3> movement;
    public static Action jump;
    public static Action sprintStart;
    public static Action sprintEnd;
    public static Action interact;
    public static Action dash;
}
