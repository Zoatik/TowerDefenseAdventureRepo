using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreeInfos", menuName = "Terrain/TreeInfos", order = 0), Serializable]
public class TreeInfos : ScriptableObject
{
    public GameObject treePrefab;
    [Range(0,1)] public float treeDensity;
    [Range(0,1)] public float heightInfluence;
    
}
