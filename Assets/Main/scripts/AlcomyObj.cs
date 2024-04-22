using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AlcomyObj : PickAbleObject
{
    [SerializeField] private IngrdentsObj alcomyObj;
    private ShopTool whereAmI;  // tracks where the object is placed

    public IngrdentsObj GetIngrdentsPotionObj() { return alcomyObj; }
    public void SetLocation(ShopTool tool)
    {
        this.whereAmI = tool;
    }

    public ShopTool WhereAmI() { return whereAmI; }

    public void DestroySelf() {

        this.transform.parent = null;
        Destroy(this.gameObject);

    }
    public void Throw(Vector3 forword, Vector3 up,float throwForce, float throwUpForce){
        this.gameObject.transform.parent = null;
        ThrowObject(forword, up, throwForce,throwUpForce);
        
    }

}
