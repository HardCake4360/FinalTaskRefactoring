using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonfire : MonoBehaviour
{
    public float range;
    new bool enabled = false;
    GameObject playerObj;
    Player m_player;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        m_player = playerObj.GetComponent<Player>();
    }

    private void Update()
    {
        if(GameManager.Instance.GetDistance(transform.position, GameManager.Instance.player.transform.position)
            < range)
        {
            if (Input.GetKeyDown(KeyCode.E) && !enabled)
            {
                GameManager.Instance.setRespawn();
                enabled = true;
                m_player.hp = m_player.maxHp;
                CanvasControl.Instance.BonfireLit();
                Debug.Log("bonfire lit");
            }
        }
    }
}
