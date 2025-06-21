using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.InitScene();
        CanvasControl.Instance.InitScene();
        //SoundManager.Instance.InitScene(1);
    }

}
