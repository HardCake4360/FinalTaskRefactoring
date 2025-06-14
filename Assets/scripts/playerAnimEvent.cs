using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimEvent : MonoBehaviour
{
    public bool attackFlag = false;
    public float damage;

    private void OnTriggerStay(Collider other)
    {
        if (attackFlag && other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.hp -= damage;
            Debug.Log("hit");
            attackFlag = false;
        }
    }
}
