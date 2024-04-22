using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class GrindRecipeSo : ScriptableObject
{
    public IngrdentsObj orginal;
    public IngrdentsObj ground;
    public int grindProgressMax;
}
