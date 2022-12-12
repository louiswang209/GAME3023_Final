using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum EncounterPhase
{
    Player,
    Opponent,
    Count
}

public class EncounterManager : MonoBehaviour
{
    public EncounterMessageManager msgManager;

    MusicManager musicM;

    EncounterPhase phase;
    public PartyDetails[] parties;

    public GameObject optionPanel;

    // Start is called before the first frame update
    void Start()
    {
        musicM = FindObjectOfType<MusicManager>();

        foreach (PartyDetails p in parties)
        {
            p.OnTurnTaken.AddListener(OnTurnTakenHandler);
        }

        for (int i = 0; i < 2; i++)
        {
            optionPanel.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = GameStats.Instance().sourceAbilities[GameStats.Instance().abilityIndices[i]].abilityName;
        }
    }

    public void AdvanceTurn()
    {
        // Player loses
        if(parties[0].health <= 0)
        {
            StartCoroutine(DelayLoseEncounter(parties[1].partyName + " has won!"));
        }
        // Player Wins
        else if(parties[1].health <= 0)
        {
            StartCoroutine(DelayWinEncounter(parties[0].partyName + " has won!"));
        }
        else
            StartCoroutine(TurnAdvanceRoutine());
    }

    IEnumerator DelayWinEncounter(string msg)
    {
        GameStats.Instance().currentHealth = parties[0].health;
        
        yield return msgManager.AnimateText(msg);
        musicM.onEncounterEndHandler();
        yield return new WaitForSeconds(2);
        GameManager.Instance().ChangeScene(1);
    }

    IEnumerator DelayLoseEncounter(string msg)
    {
        yield return msgManager.AnimateText(msg);
        musicM.onEncounterEndHandler();
        yield return new WaitForSeconds(2);
        GameStats.Instance().saveLoad.LoadGame();
    }

    IEnumerator TurnAdvanceRoutine()
    {
        phase++;
        phase = phase >= EncounterPhase.Count ? 0 : phase;
        PartyDetails forWhomTheTurnTolls = parties[(int)phase];

        yield return msgManager.AnimateText(forWhomTheTurnTolls.partyName + "'s turn!");
        yield return new WaitForSeconds(1);

        if ((int)phase == 1)
        {
            forWhomTheTurnTolls.TakeTurn(parties[0]);
        }
        else
        {
            for (int i = 0; i < optionPanel.transform.childCount; i++)
            {
                optionPanel.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void OnButtonAction(int i)
    {
        for (int j = 0; j < optionPanel.transform.childCount; j++)
        {
            optionPanel.transform.GetChild(j).gameObject.GetComponent<Button>().interactable = false;
        }

        GameStats.Instance().sourceAbilities[GameStats.Instance().abilityIndices[i]].UseAbility(parties[0], parties[1]);
        parties[0].OnTurnTaken.Invoke(parties[0].partyName, GameStats.Instance().sourceAbilities[GameStats.Instance().abilityIndices[i]].abilityName, GameStats.Instance().sourceAbilities[GameStats.Instance().abilityIndices[i]].actionSFX);
    }

    public void OnFleeButton()
    {
        musicM.PlaySound(5);
        StartCoroutine(DelayWinEncounter("Player has fled!"));
    }

    public void OnTurnTakenHandler(string name, string ability, int sound)
    {
        musicM.PlaySound(sound);
        StartCoroutine(EndTurn(name +" used " + ability));
    }

    IEnumerator EndTurn(string message)
    {
        yield return msgManager.AnimateText(message);
        yield return new WaitForSeconds(1);
        AdvanceTurn();
    }
}