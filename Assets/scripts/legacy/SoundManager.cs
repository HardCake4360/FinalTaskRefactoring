using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    
    public float vol;
    public new AudioClip[] audio;
    public AudioSource source;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void InitScene(int sceneNum )
    {
        source.Stop();
        source.PlayOneShot(audio[sceneNum], vol);
    }
    public void playSound(int soundNum)
    {
        source.PlayOneShot(audio[soundNum], vol);
    }
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
