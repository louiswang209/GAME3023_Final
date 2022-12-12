using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PartyDetails : MonoBehaviour
{
    public UnityEvent<string, string, int> OnTurnTaken;

    [Header("Basic Details")]
    public string partyName;
    public int health;
    public ActionScriptable[] actionOptions = new ActionScriptable[4];

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void TakeTurn()
    { }
    public virtual void TakeTurn(PartyDetails opponent)
    { }

    public void ModifyHealth(int damage)
    {
        health -= damage;
        health = Mathf.Max(Mathf.Min(health, 40), 0);
        healthText.text = "Grade: " + HealthToGrade(health);
    }

    protected string HealthToGrade(int health)
    {
        string grade = "";

        if (health >= 37)
            grade = "A+";
        else if (health >= 33)
            grade = "A";
        else if (health >= 30)
            grade = "A-";
        else if (health >= 27)
            grade = "B+";
        else if (health >= 23)
            grade = "B";
        else if (health >= 20)
            grade = "B-";
        else if (health >= 17)
            grade = "C+";
        else if (health >= 13)
            grade = "C";
        else if (health >= 10)
            grade = "C-";
        else if (health >= 7)
            grade = "D+";
        else if (health >= 3)
            grade = "D";
        else if (health > 0)
            grade = "D-";
        else
            grade = "F";

        return grade;
    }
}
