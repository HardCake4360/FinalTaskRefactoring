using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollinrock : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            player.GetDemaged(5);
        }
    }
}
