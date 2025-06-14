using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageWhileStep : MonoBehaviour
{
    public GameObject playerObj;
    public Player m_player;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Player");
        m_player = playerObj.GetComponent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "step" && !m_player.shoed)
            m_player.GetDamagedDeltaTime(10);
    }

}
