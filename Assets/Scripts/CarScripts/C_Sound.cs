using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class C_Sound 
{
    //Så man når dessa i inspekorn i Unity genom AudioManager
    [SerializeField] public string name;

    [SerializeField] public AudioClip clip;
 
    [SerializeField] [Range(0f, 1f)] public float volume;

    [SerializeField] [Range(.1f, 3f)] public float pitch;

    [SerializeField] public bool loop;

    [HideInInspector]
    public AudioSource source;
}
