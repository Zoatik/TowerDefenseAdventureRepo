using UnityEngine;

public class Player : MonoBehaviour {
    
    [SerializeField]
    private PlayerInfosScript _playerInfos;

    public void SetPlayerInfos(PlayerInfosScript playerInfos)
    {
        _playerInfos = playerInfos;
    }
    public PlayerInfosScript GetPlayerInfos()
    {
        return _playerInfos;
    }

    void Start()
    {
       
    }

}
