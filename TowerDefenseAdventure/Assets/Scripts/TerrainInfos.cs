using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainInfos", menuName = "Terrain/TerrainInfos", order = 0), Serializable]
public class TerrainInfos : ScriptableObject
{
    [SerializeField] public int height;
    [SerializeField] public int width;
    [SerializeField] public float depth;
    [SerializeField] public float heightsScale;
    [SerializeField, Range(0f,300f)] public float intensityOffset;
    [SerializeField, Range(0f,10f)] public float distanceIntensity;

    public static TerrainInfos CreateInstance(int height, int width, float depth, float heightsScale, float distanceIntensity, float intensityOffset)
    {
        var terrainInfos = ScriptableObject.CreateInstance<TerrainInfos>();
        terrainInfos.Init(height, width, depth, heightsScale, distanceIntensity, intensityOffset);
        return terrainInfos;
    }

    private void Init(int height, int width, float depth, float heightsScale, float distanceIntensity, float intensityOffset)
    {
        this.height = height;
        this.width = width;
        this.depth = depth;
        this.heightsScale = heightsScale;
        this.distanceIntensity = distanceIntensity;
        this.intensityOffset = intensityOffset;
    }

    public static explicit operator GameObject(TerrainInfos v)
    {
        throw new NotImplementedException();
    }

    public static explicit operator TerrainInfos(Component v)
    {
        throw new NotImplementedException();
    }
}

