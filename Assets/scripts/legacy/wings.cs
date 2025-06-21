using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wings : MonoBehaviour
{
    Player_R player;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject.GetComponent<Player_R>();
            player.winged = true;
            Destroy(gameObject);
        }
            
    }
}
