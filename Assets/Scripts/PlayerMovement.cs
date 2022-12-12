using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    public MusicManager musicM;

    [SerializeField]
    float speed = 5;

    [SerializeField]
    Rigidbody2D rigidBody;

    public bool hasEncounter;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        anim = GetComponent<Animator>();

        SceneManager.sceneLoaded += OnSceneLoaded;
        gameObject.GetComponent<SaveLoadScript>().OnSaveGame.AddListener(OnSaveHandler);
        gameObject.GetComponent<SaveLoadScript>().OnLoadGame.AddListener(OnLoadHandler);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
        
        if (!hasEncounter)
        {
            Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movementVector *= speed;
            rigidBody.velocity = movementVector;
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
        }
    }

    void OnSaveHandler()
    {
        GameStats.Instance().LastPosition = transform.position;

        PlayerPrefs.SetFloat("xPos", transform.position.x);
        PlayerPrefs.SetFloat("yPos", transform.position.y);
        PlayerPrefs.SetFloat("zPos", transform.position.z);
    }

    void OnLoadHandler()
    {
        Vector3 loadPos = new Vector3();

        if (PlayerPrefs.HasKey("xPos"))
        {
            loadPos.x = PlayerPrefs.GetFloat("xPos");
        }
        if (PlayerPrefs.HasKey("yPos"))
        {
            loadPos.y = PlayerPrefs.GetFloat("yPos");
        }
        if (PlayerPrefs.HasKey("zPos"))
        {
            loadPos.z = PlayerPrefs.GetFloat("zPos");
        }

        transform.position = loadPos;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            hasEncounter = false;

            transform.position = GameStats.Instance().LastPosition;
        }
        else
        {
            transform.position = new Vector3(100, 100, 100);
        }
    }
}
