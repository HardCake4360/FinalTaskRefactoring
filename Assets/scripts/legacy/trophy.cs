using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trophy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.clear = true;
            Destroy(gameObject);
        }
    }
}
