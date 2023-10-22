using System;
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

    [NonSerialized] public Player player;
    private Base baseBuilding;
    private List<Enemy> enemies = new();
    private List<GameObject> spawnPoints = new();
    private Terrain terrain;
    private GameManager gameManager;
    [SerializeField] private int spawnPointsNumber = 1;//à récupérer en fct du nb de waves et du niveau


    void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    public IEnumerator Spawn()
    {
        //terrain instantiaton
        terrain = Instantiate(terrainPrefab);

        //base instantiation
        yield return new WaitForSeconds(1);
        Vector3 basePosition = new(0,terrain.terrainData.GetHeight(128,128),0);
        baseBuilding = Instantiate(baseBuildingPrefab,basePosition, transform.rotation);
        baseBuildingPrefab.transform.position = new(0,200,0);

        //Player Instantiation
        player = Instantiate(playerPrefab, baseBuilding.playerSpawnPoint);
        PlayerInfos playerInfos = gameManager.LoadPlayerInfos();
        KeyBindings keyBindings = gameManager.LoadKeyBindings();
        if(playerInfos != null)
            player.SetPlayerInfos(gameManager.LoadPlayerInfos());
        if(keyBindings != null)
            player.inputManager.keyBindings = gameManager.LoadKeyBindings();

        //spawnPoints of the enemies instantiation
        for (int i = 0; i < spawnPointsNumber; i++)
        {
            spawnPoints.Add(Instantiate(spawnPointPrefab, FindSpawnPosition(), transform.rotation));
        }
        
        StartCoroutine(SpawnEnemies());
    }
    void Update()//DEBUG
    {
        float posY = terrain.transform.position.y;
        float posyZ = terrain.transform.position.z;
        float newPosX = terrain.transform.position.x + Time.deltaTime;
        terrain.transform.position.Set(newPosX, posY, posyZ);
    }
    IEnumerator SpawnEnemies()
    {
        //enemies instantiation
        foreach (var infos in spawnInfos)
        {
            for (int i = 0; i < infos.enemyNumber; i++)//boucle sur le nombre d'ennemi du meme type
            {
                int spawnPointIndex = UnityEngine.Random.Range(0,spawnPoints.Count);
                Vector3 enemyPosition = spawnPoints[spawnPointIndex].transform.position;
                enemies.Add(Instantiate(enemyPrefab,enemyPosition, transform.rotation));
                enemies[^1].Set(infos.enemyInfos);
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    private Vector3 FindSpawnPosition()
    {
        float angle = UnityEngine.Random.Range(0.0f, 2*Mathf.PI);
        float minDist = baseBuilding.GetNonSpawnRange();
        float maxDist = terrain.terrainData.size.x / 2; //demi coté du terrain (potentiellement changer en variable)
        float dist = UnityEngine.Random.Range(minDist,maxDist);
        Vector3 relativePos = new(Mathf.Cos(angle)*dist,0, Mathf.Sin(angle)*dist);
        Vector3 absolutePos = relativePos + baseBuilding.transform.position;
        Ray ray = new(new Vector3(absolutePos.x,200,absolutePos.z), Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag("Floor"))
            return hit.point;
          
        return FindSpawnPosition();
    }
}
