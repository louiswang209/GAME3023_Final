using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentPartyDetails : PartyDetails
{
    [Header("Opponent Details")]
    public Image oppSprite;
    public OpponentListScriptable oppList;
    public OpponentScriptable currentOpponent;

    // Start is called before the first frame update
    void Start()
    {
        currentOpponent = oppList.RandomOpponent();
        oppSprite.sprite = currentOpponent.oppSprite;
        partyName = currentOpponent.oppName;
        health = Random.Range(currentOpponent.minHealth, 40);
        nameText.text = partyName;
        healthText.text = "Grade: " + HealthToGrade(health);
    }

    public override void TakeTurn()
    {

    }

    public override void TakeTurn(PartyDetails opponent)
    {
        /*
         * Opponent's attacks are all the same as the Player's attacks, just named differently for theming
         * 
         * 0 - Debug        regular attack
         * 1 - Break        healing move
         * 2 - Pop Quiz     bonus damage
         * 3 - Teach        bonus healing
         */
        string abilUsed = "";
        int soundUsed = 0;

        int det = health - opponent.health;
        float rand = Random.Range(0.0f, 100.0f);

        if (det < -10)
        {
            if (rand <= 30)
            {
                actionOptions[1].UseAbility(this, opponent);
                abilUsed = actionOptions[1].abilityName;
                soundUsed = actionOptions[1].actionSFX;
            }
            else
            {
                actionOptions[3].UseAbility(this, opponent);
                abilUsed = actionOptions[3].abilityName;
                soundUsed = actionOptions[3].actionSFX;
            }
        }
        else if(det >= -9 && det <= -4)
        {
            if (rand <= 25)
            {
                actionOptions[1].UseAbility(this, opponent);
                abilUsed = actionOptions[1].abilityName;
                soundUsed = actionOptions[1].actionSFX;
            }
            else
            {
                actionOptions[3].UseAbility(this, opponent);
                abilUsed = actionOptions[3].abilityName;
                soundUsed = actionOptions[3].actionSFX;
            }
        }
        else if(det >= 3)
        {
            actionOptions[2].UseAbility(this, opponent);
            abilUsed = actionOptions[2].abilityName;
            soundUsed = actionOptions[2].actionSFX;
        }
        else
        {
            if(rand <= 30)
            {
                actionOptions[0].UseAbility(this, opponent);
                abilUsed = actionOptions[0].abilityName;
                soundUsed = actionOptions[0].actionSFX;
            }
            else if(rand <= 60)
            {
                actionOptions[3].UseAbility(this, opponent);
                abilUsed = actionOptions[3].abilityName;
                soundUsed = actionOptions[3].actionSFX;
            }
            else if(rand <= 80)
            {
                actionOptions[1].UseAbility(this, opponent);
                abilUsed = actionOptions[1].abilityName;
                soundUsed = actionOptions[1].actionSFX;
            }
            else
            {
                actionOptions[2].UseAbility(this, opponent);
                abilUsed = actionOptions[2].abilityName;
                soundUsed = actionOptions[2].actionSFX;
            }
        }

        OnTurnTaken.Invoke(partyName, abilUsed, soundUsed);
    }
}
