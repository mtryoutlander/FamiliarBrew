using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private float movementSpeed, rotateSpeed;
    [SerializeField] private GameInput input;
    [SerializeField] private LayerMask intractiveLayer;
    [SerializeField] private Transform itemHeldPostion;
    [SerializeField] private float health, throwForce, throwUpForce;

    private Animator animator;
    private float playerRadious = .6f;
    private float playerHight = 2f;
    private bool isSprinting = false;
    private IInteractable selctedObject;
    private GameObject heldObject;

    public event EventHandler<OnHealthChangeEvent> OnHealthChange;
    public class OnHealthChangeEvent : EventArgs
    {
        public float progressNormalized;
    }


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play("Idle");
        input.OnInteractAction += Input_OnInteractAction;
        input.OnSprintAction += Input_OnSprintAction;
        input.OnSprintEndAction += Input_OnSprintEndAction;
        input.OnInteractAltAction += Input_OnInteractAltAction;
        input.OnThrow += Input_OnThrow;

    }


    private void Update()
    {
        HadleMovement();
        CheckForIntractiableObject();
    }

    private void CheckForIntractiableObject()
    {
        RaycastHit hit;
        float playerReach = 2f;
        Debug.DrawRay(transform.position, transform.forward * playerReach, Color.green);
        //make a ray cast to see if there is a intractiable Object in front of the player
        if (Physics.Raycast(transform.position, transform.forward, out hit, playerReach, intractiveLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            SetSelectedObject(interactable);
        }
        else
        {
            if (selctedObject != null)
                selctedObject.HideSelected();
            selctedObject = null;

        }
    }
    private void SetSelectedObject(IInteractable interactable)
    {
        if (interactable != null) // if there is a intractiable Object in front of the player
        {
            if (selctedObject != null)
                if (selctedObject != interactable) //make sure that the player is not looking at the same object
                    selctedObject.HideSelected();  //if so hide the last selected object
            selctedObject = interactable;          //set the new selected object
            selctedObject.ShowSelected();          //and show it

        }
        else
        {
            if (selctedObject != null)
                selctedObject.HideSelected();
            selctedObject = null;

        }

    }


    private void Input_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selctedObject == null)
            return;
        if (heldObject == null)
            heldObject = selctedObject.Interact();
        else
            heldObject = selctedObject.Interact(heldObject);
        if (heldObject != null)
        {
            heldObject.transform.position = itemHeldPostion.position;
            heldObject.transform.SetParent(itemHeldPostion);
            heldObject.GetComponent<AlcomyObj>().Interact();
        }

        

    }

    private void Input_OnThrow(object sender, EventArgs e)
    {
        if(heldObject != null)
        {
            heldObject.GetComponent<AlcomyObj>().Throw(transform.forward, transform.up,throwForce, throwUpForce);
            heldObject = null;
        }
    }

    private void Input_OnInteractAltAction(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    private void Input_OnSprintAction(object sender, EventArgs e)
    {
        isSprinting = true;

    }
    private void Input_OnSprintEndAction(object sender, EventArgs e)
    {
        isSprinting = false;
    }


    private void HadleMovement()
    {
        float currentSpeed = movementSpeed;
        if (isSprinting)
            currentSpeed *= 2;
        Vector3 moveVector = input.GetMovementVectorNormal();
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadious, moveVector, currentSpeed * Time.deltaTime);

        transform.forward = Vector3.Slerp(transform.forward, moveVector, Time.deltaTime * rotateSpeed);

        if (!canMove)
        { // can't move towards moveDir
            //try move on the x
            Vector3 moveDirX = new Vector3(moveVector.x, 0, 0);//.normalized; not sure why normalized dose not work 
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadious, moveDirX, currentSpeed * Time.deltaTime); ;
            if (canMove)
            {//can only move on the x
                moveVector = moveDirX;
            }
            else
            { //attemp move on z
                Vector3 moveDirZ = new Vector3(0, 0, moveVector.z);//.normalized; 
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadious, moveDirZ, currentSpeed * Time.deltaTime); ;
                if (canMove)
                {
                    moveVector = moveDirZ;
                }//else can't move 
            }
        }
        if (canMove)
            transform.position += moveVector * currentSpeed;
        SetAnimation(moveVector);
    }

    private void SetAnimation(Vector3 moveVector)
    {
        if (moveVector == Vector3.zero)
        {
            animator.SetFloat("Speed", 0); // speed of 0 play idle
        }
        else if (isSprinting)
        {
            animator.SetFloat("Speed", 2); // speed set 2 play sprint animation
        }
        else
        {
            animator.SetFloat("Speed", 1); // speed of 1 play walk animation

        }
    }

}
