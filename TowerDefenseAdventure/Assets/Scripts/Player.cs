using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
    
    [SerializeField]private PlayerInfos playerInfos;
    public InputManager inputManager;

    
    public void SetPlayerInfos(PlayerInfos playerInfos)
    {
        this.playerInfos = playerInfos;
    }
    public PlayerInfos GetPlayerInfos()
    {
        return playerInfos;
    }

    void Start()
    {
       
    }

}
