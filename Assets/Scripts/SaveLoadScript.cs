using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveLoadScript : MonoBehaviour
{
    public UnityEvent OnSaveGame;
    public UnityEvent OnLoadGame;

    private void Start()
    {
        GameStats.Instance().saveLoad = this;
        GameStats.Instance().SaveLoadHandler();
    }

    public void SaveGame()
    {
        Debug.Log("Game Saved");
        OnSaveGame.Invoke();
    }

    public void LoadGame()
    {
        Debug.Log("Game Loaded");
        OnLoadGame.Invoke();
    }
}
