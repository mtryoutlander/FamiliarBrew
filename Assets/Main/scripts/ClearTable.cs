using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class ClearTable : ShopTool
{
    [SerializeField] private Transform[] postions;

    public override GameObject Interact()
    {
        GameObject itemBeingReturned = null;
        foreach (Transform postion in postions){
                if (postion.childCount > 0)
                {
                    itemBeingReturned = postion.GetChild(0).gameObject;
                    postion.DetachChildren();
                    break;
                }
            }
        if(!postions.Any(x=> x.childCount > 0))
            alcomyObj = null;
        return itemBeingReturned;
    }
    public override GameObject Interact(GameObject objectBeingHeld)
    {
            return PlaceHeldObject(objectBeingHeld);
    }
    private GameObject PlaceHeldObject(GameObject objectBeingHeld)
    {

        foreach (Transform postion in postions)
        {
            if(postion.childCount == 0)
            {
                if (alcomyObj == null || alcomyObj.GetIngrdentsPotionObj().name == objectBeingHeld.GetComponent<AlcomyObj>().GetIngrdentsPotionObj().name)
                {
                    objectBeingHeld.transform.parent = postion;
                    objectBeingHeld.transform.position = postion.transform.position;
                    alcomyObj = objectBeingHeld.gameObject.GetComponent<AlcomyObj>();
                    alcomyObj.SetLocation(this);
                    alcomyObj.Interact();
                    return null;
                }
            }
        }
        return objectBeingHeld;
    }


    
}
