using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundInit : MonoBehaviour
{
    public int soundNum;
    // Update is called once per frame
    private void Start()
    {
        SoundManager.Instance.InitScene(soundNum);
    }
    void Update()
    {
        
    }
}
