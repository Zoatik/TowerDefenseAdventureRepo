using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private SpawnScript spawnScript;
    private string currentSceneName;
    private Canvas HUD;
    

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            spawnScript = GetComponent<SpawnScript>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(this);
        }
        else {Destroy(this);}
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        Debug.Log("new scene loaded : " + currentSceneName);

        if(currentSceneName == "MainMenuScene")
            MainMenuStart();    
        
        else if(currentSceneName == "LobbyScene")
            LobbyStart();
            
        else if(currentSceneName == "GameScene")   
            GameStart();

        HUD = GetComponent<Canvas>();
    }

    private void MainMenuStart()
    {
        spawnScript.enabled = false;
    }
    private void LobbyStart()
    {
        spawnScript.enabled = true;
        TerrainInfos terrainInfos =  TerrainInfos.CreateInstance(256,256,20,10,.2f,0);
        spawnScript.SpawnTerrain(terrainInfos);// a mettre au debut de game TD
        spawnScript.SpawnBase();
        spawnScript.SpawnPlayer();
    }

    void GameStart()
    {
        spawnScript.enabled = true;
        TerrainInfos terrainInfos =  TerrainInfos.CreateInstance(256,256,20,10,.2f,0);
        spawnScript.SpawnTerrain(terrainInfos);// a mettre au debut de game TD
        spawnScript.SpawnBase();
        spawnScript.SpawnPlayer();
        Debug.Log("spawn enemies");
        StartCoroutine(spawnScript.SpawnEnemies());
    }

    public void SaveAll()
    {
        Debug.Log("on save");
        SaveManager.Save(spawnScript.player);
        SaveManager.Save(spawnScript.player.inputManager.keyBindings.keyBindings);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
    public PlayerInfos LoadPlayerInfos()
    {
        PlayerInfos.PlayerData playerData = SaveManager.LoadPlayer();
        if(playerData == null)
            return null;
        return PlayerInfos.CreateInstance(SaveManager.LoadPlayer());
    }

    public PlayerSpells LoadPlayerSpells()
    {
        PlayerSpells.PlayerSpellData[] spells = SaveManager.LoadSpells();
        if(spells == null)
            return null;
        return PlayerSpells.CreateInstance(spells);
    }

    public KeyBindings LoadKeyBindings()
    {
        KeyBindings.KeyBinding[] keyBindings = SaveManager.LoadKeyBindings();
        if(keyBindings == null)
            return null;
        return KeyBindings.CreateInstance(SaveManager.LoadKeyBindings());
    }
}
