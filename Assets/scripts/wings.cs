using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wings : MonoBehaviour
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
        if (other.tag == "Player")
            player.winged = true;
        GameManager.Instance.wings = true;
        Destroy(gameObject);
    }
}
