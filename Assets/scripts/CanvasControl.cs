using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    public GameObject Root;
    private static CanvasControl instance;
    [Header("Player Object")]
    [SerializeField] private GameObject player;
    
    [Header("Status")]
    [SerializeField] private Slider UIhp;
    [SerializeField] private Slider UIstamina;

    [Header("UI")]
    [SerializeField] private GameObject bonfireLit;
    [Header("Audio Source")]
    public new AudioClip[] audio;

    [Header("Mission")]
    [SerializeField] public GameObject m1;
    [SerializeField] public GameObject m2;
    Toggle t1;
    Toggle t2;

    public bool show;
    public GameObject bossObj;
    public Enemy boss;
    public Slider bossHpBar;
    public GameObject hpShow;

    private Player m_player;

    public static CanvasControl Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public void InitScene()
    {
        player = GameObject.Find("Player");
        m_player = player.GetComponent<Player>();
        bossObj = GameObject.Find("Boss");
        boss = bossObj.GetComponent<Enemy>();
        Debug.Log("scene init");
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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        m_player = player.GetComponent<Player>();
        t1 = m1.GetComponent<Toggle>();
        t2 = m2.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.interrupt)
        {
            Root.SetActive(false);
            Debug.Log("invisible");
        }
        else
        {
            Root.SetActive(true);
            Debug.Log("visible");
        }
        if (GameManager.Instance.clear) return;
        UIhp.value = m_player.GetHpPer();
        UIstamina.value = m_player.GetStaminaPer();
        
        if (bossObj != null)
            bossHpBar.value = boss.hp/boss.maxHp;
        else
            bossHpBar.enabled = false;

        if (show && bossObj != null)
        {
            hpShow.SetActive(true);
        }
        else
        {
            hpShow.SetActive(false);
        }

        t1.isOn = GameManager.Instance.mission1;
        t2.isOn = GameManager.Instance.mission2;
    }

    public void BonfireLit()
    {
        GetComponent<AudioSource>().PlayOneShot(audio[0], 0.5f);
        StartCoroutine("BonfireLitCor");
    }

    public IEnumerator BonfireLitCor()
    {
        bonfireLit.SetActive(true);
        yield return new WaitForSeconds(4f);
        bonfireLit.SetActive(false);
    }
}