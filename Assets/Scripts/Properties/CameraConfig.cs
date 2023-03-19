using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Config")]
public class CameraConfig : ScriptableObject
{
    public float TurnSmooth;
    public float PivotSpeed;
    public float XRotateSpeed;
    public float YRotateSpeed;
    public float MinAngle;
    public float MaxAngle;
    public float NormalX;
    public float NormalY;
    public float NormalZ;
    public float AimX;
    public float AimZ;
}
