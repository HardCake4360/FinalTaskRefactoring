using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.InitScene(0);
    }
}
