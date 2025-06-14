using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleToMain : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
    public void LoadClearScene()
    {
        SceneManager.LoadScene("Clear");
    }
    public void LoadFailScene()
    {
        SceneManager.LoadScene("Fail");
    }
}
