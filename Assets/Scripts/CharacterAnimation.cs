using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public CharacterStatus characterStatus;
    public Animator animator;

    public void AnimationUpdate()
    {
        animator.SetBool("IsAiming", characterStatus.IsAiming);

        AnimateWalking();
    }

    void AnimateWalking()
    {
        animator.SetFloat("Vertical", characterMovement.vertical, 0.15f, Time.deltaTime);
        animator.SetFloat("Horizontal", characterMovement.horizontal, 0.15f, Time.deltaTime);
    }
}
