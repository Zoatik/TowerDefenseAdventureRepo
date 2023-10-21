using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    private float _spawnRate = 1f;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Base baseBuildingPrefab;
    [SerializeField] private List<SpawnInfosScript> spawnInfos;
    [SerializeField] private GameObject spawnPointPrefab;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Terrain terrainPrefab;

    private Player player;
    private Base baseBuilding;
    private List<Enemy> enemies = new();
    private List<GameObject> spawnPoints = new();
    private Terrain terrain;
    [SerializeField] private int spawnPointsNumber = 1;//à récupérer en fct du nb de waves et du niveau

    void Start()
    {
        //terrain instantiaton
        terrain = Instantiate(terrainPrefab);
        //Vector3 middlePos = new(terrain.terrainData.size.x/2, terrain.terrainData.size.y/2);
        //base instantiation
        Vector3 basePosition = new(0,terrain.terrainData.GetHeight(128,128),0);
        baseBuilding = Instantiate(baseBuildingPrefab,basePosition, transform.rotation);
        //Player Instantiation
        player = Instantiate(playerPrefab, baseBuilding.playerSpawnPoint);
        //spawnPoints of the enemies instantiation
        for (int i = 0; i < spawnPointsNumber; i++)
        {
            spawnPoints.Add(Instantiate(spawnPointPrefab, FindSpawnPosition(), transform.rotation));
        }
        
        StartCoroutine(SpawnEnemies());

    }
    IEnumerator SpawnEnemies()
    {
        //enemies instantiation
        foreach (var infos in spawnInfos)
        {
            for (int i = 0; i < infos.enemyNumber; i++)//boucle sur le nombre d'ennemi du meme type
            {
                int spawnPointIndex = Random.Range(0,spawnPoints.Count);
                Vector3 enemyPosition = spawnPoints[spawnPointIndex].transform.position;
                enemies.Add(Instantiate(enemyPrefab,enemyPosition, transform.rotation));
                enemies[^1].Set(infos.enemyInfos);
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    private Vector3 FindSpawnPosition()
    {
        Debug.Log("dfgasgda");
        float angle = Random.Range(0.0f, 2*Mathf.PI);
        float minDist = baseBuilding.GetNonSpawnRange();
        float maxDist = terrain.terrainData.size.x / 2; //demi coté du terrain (potentiellement changer en variable)
        float dist = Random.Range(minDist,maxDist);
        Vector3 relativePos = new(Mathf.Cos(angle)*dist,0, Mathf.Sin(angle)*dist);
        Vector3 absolutePos = relativePos + baseBuilding.transform.position;
        absolutePos.y = terrain.terrainData.GetHeight(Mathf.RoundToInt(absolutePos.x), Mathf.RoundToInt(absolutePos.z));
          
        return absolutePos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
