using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translateBack : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(-transform.forward*Time.deltaTime*speed);
    }
}
