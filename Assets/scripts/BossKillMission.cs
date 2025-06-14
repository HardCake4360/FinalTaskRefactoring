using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKillMission : MonoBehaviour
{
    public GameObject showWhenClear;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boss = GameObject.Find("Boss");
        if (boss == null)
        {
            GameManager.Instance.mission2 = true;
            showWhenClear.SetActive(true);
        }
    }
}
