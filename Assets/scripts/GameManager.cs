using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject player;

    public Player_R playerScript;

    public Vector3 respawn;

    public float timeLimit;
    public float maxTimeLimit;
    public float curTime;
    public float fallBorder;

    new public AudioClip[] audio;
    public Text timer;

    public bool mission1;
    public bool mission2;

    public bool interrupt;
    public bool clear;
    public bool fail;

    [Header("d")]
    [SerializeField] private GameObject bloodPrefab;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public void InitScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player_R>();
        player.transform.position = respawn;
        clear = false;
        fail = false;
        interrupt = false;
        timeLimit = maxTimeLimit;
        Debug.Log("scene init");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.transform.position = respawn;
            Debug.Log("asdfasdfkljas;dlkj");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            timeLimit += 10;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            timeLimit = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.transform.position = new Vector3(25, 32, - 46);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.transform.position = new Vector3(134, 75, -45);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerScript.hp = playerScript.maxHp;
        }

        if(player.transform.position.y < fallBorder)
        {
            playerScript.hp -= 20;
            player.transform.position = respawn;
        }

        if (clear && !interrupt)
        {
            SceneManager.LoadScene("Clear");
            Cursor.lockState = CursorLockMode.None;
            interrupt = true;
        }
        if ((timeLimit < 0 || playerScript.hp<0) 
            && !interrupt)
        {
            SceneManager.LoadScene("Fail");
            Cursor.lockState = CursorLockMode.None;
            interrupt = true;
        }
        timeLimit -= Time.deltaTime;
        timer.text = ((int)timeLimit).ToString();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Member functions

    public void ReloadScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void setRespawn()
    {
        respawn = player.transform.position;
    }

    public float GetDistance(Vector3 start,Vector3 end)
    {
        return (end - start).magnitude;
    }
    
}
