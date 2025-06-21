using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyKillMission : MonoBehaviour
{
    public GameObject enemy;
    public new string name;
    public GameObject targetBonfire;
    public GameObject targetDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.Find(name);
        if (enemy == null)
        {
            GameManager.Instance.mission1 = true;
            targetBonfire.SetActive(true);
            targetDoor.SetActive(false);
        }
    }
}
