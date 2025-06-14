using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneManager : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(loadScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadScene()
    {
        SceneManager.LoadScene("runGame");
    }
}
