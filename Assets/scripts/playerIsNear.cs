using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerIsNear : MonoBehaviour
{
    public bool player;
    //public Enemy boss;
    //public float range;
    // Start is called before the first frame update
    void Start()
    {
        //boss = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        player = CanvasControl.Instance.show;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            CanvasControl.Instance.show = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            CanvasControl.Instance.show = false;
    }
}
