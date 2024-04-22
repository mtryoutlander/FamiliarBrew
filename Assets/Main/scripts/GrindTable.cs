using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GrindTable : ShopTool
{

  
    [SerializeField] Transform storePostion;
    [SerializeField] GrindRecipeSo[] recipes;
    

    private int grindProgress;
    public override GameObject Interact()
    {
        if (alcomyObj == null)
            return null;
        return GrindIngrdent();
    }
    public override GameObject Interact(GameObject objectBeingHeld)
    {
        if (alcomyObj == null) { 
            objectBeingHeld.GetComponent<AlcomyObj>().Interact();
            return PlaceHeldObject(objectBeingHeld);
        }
        return objectBeingHeld;

    }
    private GameObject PlaceHeldObject(GameObject objectBeingHeld)
    {
        grindProgress = 0;
        this.FireProgressChageEvent((float)grindProgress, (float)1.0);
        alcomyObj = objectBeingHeld.gameObject.GetComponent<AlcomyObj>();
        objectBeingHeld.transform.parent = storePostion;
        objectBeingHeld.transform.position = storePostion.transform.position;
        alcomyObj.SetLocation(this);
        return null;
        
    }
    private GameObject GrindIngrdent()
    {
        GrindRecipeSo recipe = GetGrindRecipeSo(alcomyObj);
        if (recipe == null)
        {
            GameObject temp = alcomyObj.gameObject;
            alcomyObj = null;
            this.FireProgressChageEvent((float)1, (float) 1 ); // hides the bar
            return temp;
        }
        return ReplaceObj(recipe);
        
    }
    private GameObject ReplaceObj(GrindRecipeSo recipe)
    {
        grindProgress ++;
        this.FireProgressChageEvent((float)grindProgress, (float) recipe.grindProgressMax);
        if (grindProgress >= recipe.grindProgressMax)
        {
            alcomyObj.DestroySelf();
            return Instantiate(recipe.ground.prefab.gameObject);
        }
        else
        {
            return null;
        }
        
    }

    private GrindRecipeSo GetGrindRecipeSo(AlcomyObj inputObj)
    {
        foreach (GrindRecipeSo recipe in recipes)
        {
            if (recipe.orginal == inputObj.GetIngrdentsPotionObj())
            {
                return recipe;
            }
        }
        return null;
    }


}
