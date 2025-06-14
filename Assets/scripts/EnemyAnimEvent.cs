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
            Player player = other.GetComponent<Player>();
            player.GetDemaged(damage);
            Debug.Log("Player hit");
            attackFlag = false;
        }
    }
}
