using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTool : MonoBehaviour, IInteractable
{
    public event EventHandler<OnProgressChangeEventArgs> OnProgressChanged;
    public class OnProgressChangeEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    [SerializeField] private GameObject selected;
    protected AlcomyObj alcomyObj;


    virtual public GameObject Interact() { return null; } // interact when holding nothing 
    virtual public GameObject Interact(GameObject objectBeingHeld) { return null; } // interact when holding somthing

    public virtual void InteractAlt() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<AlcomyObj>())
        {
            this.Interact(other.gameObject);
        }
    }
    public void FireProgressChageEvent(float progress, float max) {
        this.OnProgressChanged?.Invoke(this, new OnProgressChangeEventArgs
        {
            progressNormalized = progress /max
        });
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
