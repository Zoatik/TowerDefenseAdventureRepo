using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Canvas canvas;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        
    }

    public void Settings()
    {

    }
    public void Play()
    {
        Debug.Log("play");
        SceneManager.LoadScene("LobbyScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
