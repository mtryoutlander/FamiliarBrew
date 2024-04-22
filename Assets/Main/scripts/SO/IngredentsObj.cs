using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class IngrdentsObj : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    private string objectName;

    private void Awake()
    {
        objectName = prefab.name;
    }
}
