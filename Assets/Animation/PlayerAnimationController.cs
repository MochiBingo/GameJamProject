using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    // Animation parameter names
    private readonly string IS_WALKING = "isWalking";
    private readonly string IS_RUNNING = "isRunning";
    private readonly string IS_JUMPING = "isJumping";
    private readonly string IS_DASHING = "isDashing";

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetWalking(bool isWalking)
    {
        animator.SetBool(IS_WALKING, isWalking);
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool(IS_RUNNING, isRunning);
    }

    public void TriggerJump()
    {
        animator.SetTrigger(IS_JUMPING);
    }

    public void TriggerDash()
    {
        animator.SetTrigger(IS_DASHING);
    }
}

