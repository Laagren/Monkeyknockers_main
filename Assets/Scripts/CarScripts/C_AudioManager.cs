using UnityEngine.Audio;
using System;
using UnityEngine;

public class C_AudioManager : MonoBehaviour
{
    [SerializeField] public C_Sound[] sounds;
    private C_AudioManager audioManager;

    void Awake()
    {
        audioManager = GetComponent<C_AudioManager>();
        foreach (C_Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
    }

    //private void Update()
    //{
    //    EngineSound();
    //}

    //private void EngineSound()
    //{
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        FindObjectOfType<C_AudioManager>().Play("CarSound");
    //        FindObjectOfType<C_AudioManager>().Stop("CarSoundBreak");
    //    }

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //audioManager.sounds[2].volume = 0;
    //        FindObjectOfType<C_AudioManager>().Play("CarSoundBreak");
    //        FindObjectOfType<C_AudioManager>().Stop("CarSound");
    //    }

    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        FindObjectOfType<C_AudioManager>().Play("CarHorn");
    //    }

    //}

    private void Start()
    {
        Play("BackgroundMusic");
    }

    public void Play (string name)
    {
       C_Sound s = Array.Find(sounds, s => s.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        C_Sound s = Array.Find(sounds, s => s.name == name);
        s.source.Stop();
    }

}
