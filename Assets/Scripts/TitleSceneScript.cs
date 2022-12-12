using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneScript : MonoBehaviour
{
    MusicManager musicM;

    void Start()
    {
        musicM = FindObjectOfType<MusicManager>();
    }

    public void StartGame()
    {
        if (PlayerPrefs.HasKey("xPos"))
        {
            Vector3 loadPos = new Vector3();

            if (PlayerPrefs.HasKey("xPos"))
                loadPos.x = PlayerPrefs.GetFloat("xPos");
            if (PlayerPrefs.HasKey("yPos"))
                loadPos.y = PlayerPrefs.GetFloat("yPos");
            if (PlayerPrefs.HasKey("zPos"))
                loadPos.z = PlayerPrefs.GetFloat("zPos");

            GameStats.Instance().LastPosition = loadPos;
        }
        else
        {
            GameStats.Instance().LastPosition = new Vector2(0, -18.5f);
        }

        GameManager.Instance().ChangeScene(1);
        musicM.onEncounterEndHandler();
    }
}
