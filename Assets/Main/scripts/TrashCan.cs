using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : ShopTool
{
    public override GameObject Interact()
    {
        return null;
    }
    public override GameObject Interact(GameObject objectBeingHeld)
    {
        objectBeingHeld.GetComponent<AlcomyObj>().DestroySelf();
        return null;

    }
}
