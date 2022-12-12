using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Prints messages over time during an encounter
/// </summary>
public class EncounterMessageManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float textSpeed;

    public IEnumerator animateTextRoutine = null;

    public IEnumerator AnimateText(string message)
    {
        if(animateTextRoutine != null)
        {
            StopCoroutine(animateTextRoutine);
        }

        animateTextRoutine = AnimateTextRoutine(message);
        StartCoroutine(animateTextRoutine);
        return animateTextRoutine;
    }

    IEnumerator AnimateTextRoutine(string message)
    {
        Assert.IsTrue(textSpeed > float.Epsilon);

        string currentMessage = "";

        for(int currentChar = 0; currentChar < message.Length; currentChar++)
        {
            currentMessage += message[currentChar];
            text.text = currentMessage;
            yield return new WaitForSeconds(1 / textSpeed);
        }

        animateTextRoutine = null;
    }
}
