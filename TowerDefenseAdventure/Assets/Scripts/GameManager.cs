using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SpawnScript spawnScript;
    
    
    void Awake()
    {
        spawnScript = GetComponent<SpawnScript>();
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(spawnScript.Spawn());// a mettre au debut de game TD
    }

    public void SaveAll()
    {
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

    public KeyBindings LoadKeyBindings()
    {
        KeyBindings.KeyBinding[] keyBindings = SaveManager.LoadKeyBindings();
        if(keyBindings == null)
            return null;
        return KeyBindings.CreateInstance(SaveManager.LoadKeyBindings());
    }
}
