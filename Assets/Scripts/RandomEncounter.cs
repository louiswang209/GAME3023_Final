using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class RandomEncounter : MonoBehaviour
{
    public GameObject player;
    private Vector2 position;
    public float encounterDelay;

    public MusicManager musicM;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        musicM = FindObjectOfType<MusicManager>();
    }

    private void EncounterEvent()
    {
        int r = Random.Range(0, 100);
        if (r <= 20)
        {
            GameStats.Instance().LastPosition = player.transform.position;
            player.GetComponent<PlayerMovement>().hasEncounter = true;
            Debug.Log("Encounter Encountered!");
            StartCoroutine(DelayedEncounter());
            musicM.onEncounterEnterHandler();
        }
    }

    IEnumerator DelayedEncounter()
    {
        yield return new WaitForSeconds(encounterDelay);
        GameManager.Instance().ChangeScene(2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            position = player.transform.position;
            EncounterEvent();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            if ((Mathf.Abs(player.transform.position.x) - Mathf.Abs(position.x) >= 1) ||
                (Mathf.Abs(player.transform.position.x) - Mathf.Abs(position.x) <= -1) ||
                    (Mathf.Abs(player.transform.position.y) - Mathf.Abs(position.y) >= 1) ||
                    (Mathf.Abs(player.transform.position.y) - Mathf.Abs(position.y) <= -1))
            {
                EncounterEvent();
                position = player.transform.position;
            }
        }
    }
}
