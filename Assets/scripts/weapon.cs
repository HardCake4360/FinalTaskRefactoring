using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    GameObject playerObj;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        player.armed = true;
        GameManager.Instance.weapon = true;
        Destroy(gameObject);
    }
}
