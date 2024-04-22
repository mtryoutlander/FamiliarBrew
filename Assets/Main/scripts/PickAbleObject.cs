using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAbleObject : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject selected;
    [SerializeField] private Rigidbody rb;

    public GameObject Interact()
    {
        rb.useGravity = false;
        rb.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        return this.gameObject;
    }
    public void ThrowObject(Vector3 forword, Vector3 up, float throwForce, float throwUpForce)
    {
        rb.useGravity = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        rb.AddForce(forword * throwForce + up *throwUpForce, ForceMode.Impulse);
    }

    public GameObject Interact(GameObject objectBeingHeld)
    {
        return objectBeingHeld;
    }

    public void ShowSelected()
    {
        selected.SetActive(true);
    }
    public void HideSelected()
    {
        selected.SetActive(false);
    }
}
