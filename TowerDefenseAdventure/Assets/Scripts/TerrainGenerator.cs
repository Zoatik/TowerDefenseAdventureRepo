using System;
using System.Collections.Generic;
using sc.terrain.proceduralpainter;
using Unity.AI.Navigation;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainGenerator : MonoBehaviour
{
    public TerrainInfos terrainInfos;
    public List<TerrainLayer> terrainLayers;
    public List<TreeInfos> treesInfos;
    [Range(0,1)] public float treeTreshHold = .5f;
    private const int maxTrees = 1000;
    [SerializeField, Range(0f,1f)] private float treesDensity = 0f;

    private Terrain terrain;
    private NavMeshSurface navMeshSurface;
    private List<GameObject> trees = new();
    private TerrainPainter terrainPainter;
    void Start()
    {
        //terrain
        terrainPainter = GetComponent<TerrainPainter>();
        terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        transform.position = new Vector3(-terrainInfos.width/2,0,-terrainInfos.width/2);
        navMeshSurface = GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
        //trees
        treeTreshHold = Mathf.Clamp(treeTreshHold,0f,1f);
        
    }
    /*void Update()//DEBUG
    {
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }*/

    private TerrainData GenerateTerrain(TerrainData baseTerrainData)
    {
        baseTerrainData.heightmapResolution = terrainInfos.width + 1;
        baseTerrainData.size = new Vector3(terrainInfos.width, terrainInfos.depth, terrainInfos.height);
        float[,] heightValues = GenerateHeights();
        baseTerrainData.SetHeights(0,0,heightValues);

        //terrain Painting
        /*terrainPainter.AssignActiveTerrains();
        terrainPainter.CreateSettingsForLayer(terrainLayers[0]);
        //terrainPainter.layerSettings.Add()
        terrainPainter.SetTerrainLayers();
        terrainPainter.autoRepaint = false;*/

        //arbres
        GenerateTrees(heightValues);
        return baseTerrainData;
    }

    //Algo spawn des arbres
    private void GenerateTrees(float[,] heightValues)
    {
        Debug.Log("heightValue : " + heightValues.GetLength(0)*heightValues.GetLength(1));
        if(treesDensity == 0.0f)
            return;
        int step = Mathf.RoundToInt(8 / treesDensity);
        for (int x = 0; x < heightValues.GetLength(0); x += step)
        {
            for (int z = 0; z < heightValues.GetLength(1); z += step)
            {
                int mostLikelyTreeIndex = 0;
                float bestTreeProbabilityScore = 0;
                for (int i = 0; i < treesInfos.Count; i++)
                {
                    float heightInfluence = heightValues[x,z] * Mathf.Pow(treesInfos[i].heightInfluence,2) - treesInfos[i].heightInfluence + 1;
                    float probabilityScore = Mathf.Abs(heightInfluence * treesInfos[i].treeDensity * UnityEngine.Random.Range(0f,1f));
                    if(probabilityScore > bestTreeProbabilityScore)
                    {
                        bestTreeProbabilityScore = probabilityScore;
                        mostLikelyTreeIndex = i;
                    }
                }
                //Debug.Log(bestTreeProbabilityScore);
                if(bestTreeProbabilityScore > treeTreshHold )
                {
                    
                    TreeInfos tmpTree = treesInfos[mostLikelyTreeIndex];
                    
                    float randAngle = UnityEngine.Random.Range(0,360);
                    float dist = step/2;
                    Vector2 mapPos = new(x, z);
                    Vector2 randPos = new(dist*Mathf.Cos(randAngle), dist*Mathf.Sin(randAngle));
                    randPos += mapPos;
                    Vector3 rayOrigin = new(randPos.x, 200, randPos.y);
                    Ray ray = new(rayOrigin, Vector3.down);
                    if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag("Floor"))
                    {
                        Vector3 n = terrain.terrainData.GetInterpolatedNormal(randPos.x / terrainInfos.width, randPos.y / terrainInfos.height);
                        Quaternion treeRotation = Quaternion.FromToRotation(Vector3.up, n);
                        Vector3 rotInit = tmpTree.treePrefab.transform.rotation.eulerAngles;
                        Vector3 yOffset = new(0f,.5f,0f);
                        trees.Add(Instantiate(tmpTree.treePrefab, hit.point - yOffset, treeRotation));
                        trees[^1].transform.SetParent(transform);
                        trees[^1].transform.Rotate(rotInit);
                    }

                }
            }
        }
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[terrainInfos.width, terrainInfos.width];
        for (int x = 0; x < terrainInfos.width; x++)
        {
            for (int y = 0; y < terrainInfos.height; y++)
            {
                heights[x,y] = CalculateHeights(x,y);
            }
        }
        return heights;
    }

    private float CalculateHeights(int x, int y)
    {
        Vector3 point = new((float)x-terrainInfos.width/2,(float)y-terrainInfos.height/2);
        //Vector3 pointInSpace = new((float)x / terrainInfos.width, (float)y / terrainInfos.height);
        float scale = terrainInfos.heightsScale;
        float xCoord = (float)x / terrainInfos.width * scale;
        float yCoord = (float)y / terrainInfos.height * scale;
        float depthScale = point.magnitude;
        float maxRange = terrainInfos.width / Mathf.Sqrt(2);     
        depthScale /= maxRange;
        depthScale *= terrainInfos.distanceIntensity;

        depthScale = Mathf.Exp(depthScale)/Mathf.Exp(terrainInfos.distanceIntensity);
        
           
        return depthScale * Mathf.PerlinNoise(xCoord,yCoord);
    }
}

