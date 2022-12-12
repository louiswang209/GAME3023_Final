using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartyDetails : PartyDetails
{
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = partyName;
        health = GameStats.Instance().currentHealth;
        healthText.text = "Grade: " + HealthToGrade(health);
    }
}
