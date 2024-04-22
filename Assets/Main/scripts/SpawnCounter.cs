using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCounter : ShopTool
{
    [SerializeField] private IngrdentsObj spawnableObject;
    private void Start()
    {
        alcomyObj = spawnableObject.prefab.GetComponent<AlcomyObj>();
    }
    public override GameObject Interact(GameObject objectBeingHeld)
    {
        return objectBeingHeld;
    }

    public override GameObject Interact()
    {
        return Instantiate(alcomyObj.gameObject);
    }


}
