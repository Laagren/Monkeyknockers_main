using UnityEngine.Audio;
using System;
using UnityEngine;

public class C_AudioManager : MonoBehaviour
{
    [SerializeField] public C_Sound[] sounds;

    /// <summary>
    /// Här ger vi scriptet C_Sound värde
    /// </summary>
    void Awake()
    {       
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

    /// <summary>
    /// Så att man kan skriva vad man döpt ljudfilen till istället för att skriva ljudfilens orginalnamn och att man kan starta ljudet.
    /// </summary>
    /// <param name="name"></param>
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
