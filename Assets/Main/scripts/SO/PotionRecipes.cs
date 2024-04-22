using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class PotionRecipes : ScriptableObject
{
    public IngrdentsObj[] ingrdents;
    public IngrdentsObj finishedPotion;
    public float craftTime;
}
