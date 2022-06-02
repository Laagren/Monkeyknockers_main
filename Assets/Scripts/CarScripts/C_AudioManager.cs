using UnityEngine.Audio;
using System;
using UnityEngine;

public class C_AudioManager : MonoBehaviour
{
    [SerializeField] public C_Sound[] sounds;

    /// <summary>
    /// H�r ger vi scriptet C_Sound v�rde
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
    /// S� att man kan skriva vad man d�pt ljudfilen till ist�llet f�r att skriva ljudfilens orginalnamn och att man kan starta ljudet.
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
