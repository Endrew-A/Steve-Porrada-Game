using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public static AudioControl Instance { get; private set; }

    public List<AudioSource> audios;




    private void Awake()

    {

        if (Instance != null && Instance != this)

        {

            Destroy(this);

        }

        else

        {

            Instance = this;

        }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyAudio()
    {
        foreach(AudioSource audio in audios)
        {
            audio.volume = PlayerPrefs.GetFloat("volume");
        }
    }
}
