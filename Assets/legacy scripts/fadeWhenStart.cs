using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeWhenStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer vis = this.GetComponent<MeshRenderer>();
        vis.enabled = false;
    }
}
