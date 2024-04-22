using UnityEngine;
interface IInteractable
{
    GameObject Interact(GameObject objectBeingHeld);
    GameObject Interact();
    void ShowSelected();
    void HideSelected();
}