using UnityEngine.Audio;
using System;
using UnityEngine;

public class C_AudioManager : MonoBehaviour
{
    [SerializeField] public C_Sound[] sounds;

    void Awake()
    {
        //Här ger vi scriptet C_Sound värde
        foreach (C_Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
   
    public void StartBackgroundMusic()
    {
        Play("BackgroundMusic");
    }

    //Så att man kan skriva vad man döpt ljudfilen till istället för att skriva ljudfilens orginalnamn och att man kan starta ljudet.
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
