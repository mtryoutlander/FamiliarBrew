using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class CraftingPot : ShopTool
{
    [SerializeField] private Transform [] storePostion;
    [SerializeField] private PotionRecipes[] recipes;
    [SerializeField] private IngrdentsObj[] failPotions;
    [SerializeField] private int failCraftTime;
    // Start is called before the first frame update

    private float brewProgress = 0;
    private bool doneBrewing, brewing;
    private List<IngrdentsObj> brewIngrdents = new List<IngrdentsObj>();
    private PotionRecipes selectedReicpe;
    public override GameObject Interact()
    {
        if (doneBrewing == true)
        {
            doneBrewing = false;
            GameObject temp = alcomyObj.gameObject;
            alcomyObj = null;
            return temp;
        }
        return null;
    }
    public override GameObject Interact(GameObject objectBeingHeld)
    {

        if (alcomyObj != null)
        {
            return objectBeingHeld;
        }
        if (brewIngrdents.Count >=3)
        {
            return objectBeingHeld;
        }
        PlaceIngredent(objectBeingHeld);
        Brew();
        return null;

    }

    private void PlaceIngredent(GameObject objectBeingHeld)
    {
        objectBeingHeld.GetComponent<AlcomyObj>().Interact();
        brewIngrdents.Add(objectBeingHeld.GetComponent<AlcomyObj>().GetIngrdentsPotionObj());
        foreach (Transform postion in storePostion)
        {
            if (postion.childCount == 0)
            {
                objectBeingHeld.transform.parent = postion;
                objectBeingHeld.transform.position = postion.transform.position;  
            }
        }
    }

    private void Update()
    {
        if (brewing)
        {
            brewProgress += Time.deltaTime;
            
        }
        if (selectedReicpe == null)
        {
            this.FireProgressChageEvent(brewProgress, failCraftTime);
            if (brewProgress >= failCraftTime)
            {
                FinishBrewing();
                alcomyObj = Instantiate( failPotions[UnityEngine.Random.Range(0,failPotions.Length-1)].prefab.gameObject).GetComponent<AlcomyObj>();
                alcomyObj.gameObject.transform.position = storePostion[1].position; // the middle postion till i get ui elements
            }
        }
        else
        {
            this.FireProgressChageEvent(brewProgress, selectedReicpe.craftTime);
            if (brewProgress >= selectedReicpe.craftTime)
            {
                FinishBrewing();
                alcomyObj = Instantiate(selectedReicpe.finishedPotion.prefab.gameObject).GetComponent<AlcomyObj>();
                alcomyObj.gameObject.transform.position = storePostion[1].position; // the middle postion till i get ui elements

            }
        }
    }
    private void FinishBrewing()
    {

        brewing = false;
        brewProgress = 0;
        doneBrewing = true;
        brewIngrdents.Clear();
        foreach(var postion in storePostion)
        {
            if(postion.childCount > 0)
            {
                postion.GetChild(0).gameObject.GetComponent<AlcomyObj>().DestroySelf();
                postion.DetachChildren();
            }
        }
    }
    private void Brew()
    {
        brewProgress = 0;
        if (CanBrew())
        {
            brewing = true;
        }
        else
        {
            selectedReicpe = null;
            if (brewIngrdents.Count >= 3)
                brewing = true;
            
        }
    }

    private bool CanBrew()
    {
        foreach (var recipe in recipes)
        {
            if (recipe.ingrdents.Length == brewIngrdents.Count)
            {
                List<IngrdentsObj> copyIngrdents = brewIngrdents;
                foreach (var ingrdent in recipe.ingrdents)
                {
                    if(copyIngrdents.Contains(ingrdent))
                        copyIngrdents.Remove(ingrdent);
                }
                if(copyIngrdents.Count == 0)
                {
                    selectedReicpe = recipe;
                    return true;
                }

            }
        }
        return false;

    }
}
