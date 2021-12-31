using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    public AudioSource source;
    public Button mute;
    public Sprite muteImage;
    public Sprite playImage;
    public Slider sfx;
    public AudioLowPassFilter lowPass;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        source = GetComponent<AudioSource>();
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void OnVolumeChanged()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = sfx.value;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Pitch(string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = pitch;
    }
    
    public void Mute()
    {
        source.mute = !source.mute;
        if(source.mute)
            mute.image.sprite = muteImage;
        else
            mute.image.sprite = playImage;
    }
}