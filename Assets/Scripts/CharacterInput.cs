using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public CharacterStatus characterStatus;

    public bool isAiming;
    public bool debugAiming;

    public void InputUpdate()
    {
        if (!debugAiming)
            characterStatus.IsAiming = Input.GetMouseButton(1);
        else
            characterStatus.IsAiming = isAiming;
    }
}
