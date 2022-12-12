using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    private static GameManager instance;
    private GameManager()
    { }

    public static GameManager Instance()
    {
        if (instance == null)
            instance = new GameManager();

        return instance;
    }


    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
