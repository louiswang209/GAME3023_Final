using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreenUI : MonoBehaviour
{
    public SaveLoadScript saveLoad;
    public GameObject abilityPanel;
    public GameObject[] abilityToggles;

    private Queue<int> abilityQueue = new Queue<int>();

    private void Start()
    {
        saveLoad = FindObjectOfType<SaveLoadScript>();

        ResetToggles();

        SceneManager.sceneLoaded += OnSceneLoaded;
        saveLoad.OnSaveGame.AddListener(OnSaveGameHandler);
        saveLoad.OnLoadGame.AddListener(OnLoadGameHandler);
    }

    public void OnSaveClicked()
    {
        saveLoad.SaveGame();
    }

    public void OnLoadClicked()
    {
        saveLoad.LoadGame();
    }

    public void OnAbilitiesClicked()
    {
        if (abilityPanel.activeSelf)
            abilityPanel.SetActive(false);
        else
            abilityPanel.SetActive(true);
    }

    public void OnToggleAbility(int i)
    {
        if (abilityToggles[i].GetComponent<Toggle>().isOn)
        {
            GameStats.Instance().AddAbility(i);
            abilityToggles[i].GetComponent<Toggle>().isOn = true;
            abilityToggles[i].GetComponent<Toggle>().interactable = false;

            abilityQueue.Enqueue(i);
            if (abilityQueue.Count > 2)
            {
                int toggle = abilityQueue.Dequeue();
                abilityToggles[toggle].GetComponent<Toggle>().isOn = false;
                abilityToggles[toggle].GetComponent<Toggle>().interactable = true;
            }
        }
    }

    private void ResetToggles()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == GameStats.Instance().abilityIndices[0] || i == GameStats.Instance().abilityIndices[1])
            {
                abilityToggles[i].GetComponent<Toggle>().isOn = true;
                abilityToggles[i].GetComponent<Toggle>().interactable = false;
            }
            else
            {
                abilityToggles[i].GetComponent<Toggle>().isOn = false;
                abilityToggles[i].GetComponent<Toggle>().interactable = true;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainScene")
        {
            ResetToggles();
        }
    }

    public void OnSaveGameHandler()
    {
        PlayerPrefs.SetInt("FirstQueue", abilityQueue.Dequeue());
        PlayerPrefs.SetInt("SecondQueue", abilityQueue.Dequeue());
        // Reset the queue from the saved values
        abilityQueue.Enqueue(PlayerPrefs.GetInt("FirstQueue"));
        abilityQueue.Enqueue(PlayerPrefs.GetInt("SecondQueue"));
    }

    public void OnLoadGameHandler()
    {
        ResetToggles();

        if (PlayerPrefs.HasKey("FirstQueue"))
        {
            abilityQueue.Enqueue(PlayerPrefs.GetInt("FirstQueue"));
            abilityQueue.Enqueue(PlayerPrefs.GetInt("SecondQueue"));
        }
    }
}
