using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class C_Sound 
{
    [SerializeField] public string name;

    [SerializeField] public AudioClip clip;
 
    [SerializeField] [Range(0f, 1f)] public float volume;

    [SerializeField] [Range(.1f, 3f)] public float pitch;

    [SerializeField] public bool loop;

    [HideInInspector]
    public AudioSource source;
}
