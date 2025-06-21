using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    public bool attackFlag = false;
    public float damage;

    private void OnTriggerStay(Collider other)
    {
        if (attackFlag && other.tag == "Player")
        {
            Player_R player = other.GetComponent<Player_R>();
            player.GetDemaged(damage);
            Debug.Log("Player hit");
            attackFlag = false;
        }
    }
}
