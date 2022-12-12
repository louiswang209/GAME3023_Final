using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameStats
{
    public ActionScriptable[] sourceAbilities = new ActionScriptable[4];
    
    private static GameStats instance;
    private GameStats()
    {
        sourceAbilities[0] = (ActionScriptable)Resources.Load("Guess");
        sourceAbilities[1] = (ActionScriptable)Resources.Load("Study");
        sourceAbilities[2] = (ActionScriptable)Resources.Load("Cheat");
        sourceAbilities[3] = (ActionScriptable)Resources.Load("Bonus");
        abilityIndices[0] = 0;
        abilityIndices[1] = 1;
    }

    public static GameStats Instance()
    {
        if (instance == null)
            instance = new GameStats();

        return instance;
    }

    public SaveLoadScript saveLoad;
    public int sceneIndex;
    
    private Vector3 lastPosition;
    public Vector3 LastPosition
    {
        get
        {
            return lastPosition;
        }
        set
        {
            lastPosition = value;
        }
    }

    public int currentHealth = 40;
    public int maxHealth = 40;

    public int[] abilityIndices = new int[2];
    int lastAbility = 0; // Can only be 0 or 1

    public void AddAbility(int actScript)
    {
        abilityIndices[lastAbility] = actScript;
        lastAbility = lastAbility + 1 == 2 ? 0 : lastAbility + 1;
    }

    public void SaveLoadHandler()
    {
        saveLoad.OnSaveGame.AddListener(SaveStats);
        saveLoad.OnLoadGame.AddListener(LoadStats);
    }

    void SaveStats()
    {
        PlayerPrefs.SetInt("health", currentHealth);

        PlayerPrefs.SetInt("Ability1", abilityIndices[0]);
        PlayerPrefs.SetInt("Ability2", abilityIndices[1]);

        PlayerPrefs.Save();
    }

    void LoadStats()
    {
        if (PlayerPrefs.HasKey("health"))
            currentHealth = PlayerPrefs.GetInt("health");

        if (PlayerPrefs.HasKey("Ability1"))
            abilityIndices[0] = PlayerPrefs.GetInt("Ability1");
        if (PlayerPrefs.HasKey("Ability2"))
            abilityIndices[1] = PlayerPrefs.GetInt("Ability2");

        GameManager.Instance().ChangeScene(1);
    }
}
