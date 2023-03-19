using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public CharacterStatus characterStatus;
    public Animator animator;

    public float vertical;
    public float horizontal;
    public float moveAmount;
    public float rotationSpeed;

    private Vector3 rotationDirection;
    private Vector3 moveDirection;

    public void MoveUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

        Vector3 moveDirection = cameraTransform.forward * vertical;
        moveDirection += cameraTransform.right * horizontal;
        moveDirection.Normalize();
        this.moveDirection = moveDirection;
        rotationDirection = cameraTransform.forward;

        RotationNormal();
        characterStatus.IsGrounding = GetGrounding();
    }

    public void RotationNormal()
    {
        /*if (!CharacterStatus.IsAiming)
        {
            RotationDirection = MoveDirection;
        }*/

        Vector3 TargetDirection = rotationDirection;
        TargetDirection.y = 0;

        if (TargetDirection == Vector3.zero)
            TargetDirection = transform.forward;

        Quaternion lookDirection = Quaternion.LookRotation(TargetDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, lookDirection, rotationSpeed);
        transform.rotation = targetRotation;
    }

    public bool GetGrounding()
    {
        Vector3 origin = transform.position;
        origin.y += 0.6f;
        Vector3 direction = Vector3.down;
        float distance = 0.7f;
        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, distance))
        {
            Vector3 tp = hit.point;
            transform.position = tp;
            return true;
        }

        return false;
    }
}
