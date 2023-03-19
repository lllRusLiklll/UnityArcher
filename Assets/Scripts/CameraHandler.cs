using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform pivot;
    public Transform character;
    public Transform mainTranform;

    public CharacterStatus characterStatus;
    public CameraConfig cameraConfig;
    public bool isLeftPivot;
    public float delta;

    private float mouseX;
    private float mouseY;
    private float smoothX;
    private float smoothY;
    private float smoothXVelocity;
    private float smoothYVelocity;
    private float lookAngle;
    private float titleAngle;

    private void Update()
    {
        FixedTick();
    }

    void FixedTick()
    {
        delta = Time.deltaTime;

        HandlePosition();
        HandleRotation();

        Vector3 targetPosition = Vector3.Lerp(mainTranform.position, character.position, 1);
        mainTranform.position = targetPosition;
    }

    void HandlePosition()
    {
        float targetX = cameraConfig.NormalX;
        float targetY = cameraConfig.NormalY;
        float targetZ = cameraConfig.NormalZ;

        if (characterStatus.IsAiming)
        {
            targetX = cameraConfig.AimX;
            targetZ = cameraConfig.AimZ;
        }

        if (isLeftPivot)
            targetX = -targetX;

        Vector3 newPivotPosition = pivot.localPosition;
        newPivotPosition.x = targetX;
        newPivotPosition.y = targetY;

        Vector3 newCameraPosition = cameraTransform.localPosition;
        newCameraPosition.z = targetZ;

        float t = delta * cameraConfig.PivotSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newCameraPosition, t);
    }

    void HandleRotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (cameraConfig.TurnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXVelocity, cameraConfig.TurnSmooth);
            smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYVelocity, cameraConfig.TurnSmooth);
        }
        else
        {
            smoothX = mouseX;
            smoothY = mouseY;
        }

        lookAngle += smoothX * cameraConfig.XRotateSpeed;
        Quaternion targetRotation = Quaternion.Euler(0, lookAngle, 0);
        mainTranform.rotation = targetRotation;

        titleAngle -= smoothY * cameraConfig.YRotateSpeed;
        titleAngle = Mathf.Clamp(titleAngle, cameraConfig.MinAngle, cameraConfig.MaxAngle);
        pivot.localRotation = Quaternion.Euler(titleAngle, 0, 0);
    }
}
