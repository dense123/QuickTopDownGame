using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //public static AudioManager instance; // Game manager will be the only singleton

    public Sound[] sounds;


    private void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }


    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"{name} not found");
            return;
        }
        if(s.TURN_THIS_OFF == true)
        {
            Debug.LogWarning($"{name} is turned off under inspector");
            return;
        }
        s.source.Play();
    }
}
