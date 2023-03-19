using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public CharacterAnimation characterAnimation;
    public CharacterInput characterInput;
    public CharacterStatus characterStatus;

    public Transform arrowInHand;
    public GameObject arrowPrefab;

    public Animator animator;

    public float force;

    void Update()
    {
        characterMovement.MoveUpdate();
        characterAnimation.AnimationUpdate();
        characterInput.InputUpdate();

        Physics.IgnoreCollision(arrowInHand.GetComponent<Collider>(), GetComponent<Collider>());

        if (characterStatus.IsAiming) 
        {
            arrowInHand.gameObject.SetActive(true);
            if (Input.GetMouseButtonUp(0))
            {
                GameObject newArrow = Instantiate(arrowPrefab, arrowInHand.position, arrowInHand.rotation);
                newArrow.GetComponent<Rigidbody>().isKinematic = false;
                newArrow.GetComponent<Rigidbody>().AddForce(arrowInHand.up * force, ForceMode.Impulse);
            }
        }
        else
            arrowInHand.gameObject.SetActive(false);
    }
}
