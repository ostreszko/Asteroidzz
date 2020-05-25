using System.Collections.Generic;
using UnityEngine;
using System;

//Class responsible for sound playing
public class AudioManager : MonoBehaviour
{
    public List<Sound> soundList;
    public static AudioManager audioManager;
    private void Awake()
    {
        if (audioManager == null)
        {
            foreach (Sound s in soundList)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;

                s.source.loop = s.loop;
            }
            audioManager = this;
            Play("BackgroundMusic");
            DontDestroyOnLoad(this);

        }
        else
        {
            GameObject.Destroy(audioManager);
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(soundList.ToArray(), sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("There is no sound with" + name + " name");
            return;
        }
        s.source.Play();
    }

    public void PlayContinously(string name)
    {
        Sound s = Array.Find(soundList.ToArray(), sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("There is no sound with" + name + " name");
            return;
        }
        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(soundList.ToArray(), sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("There is no sound with" + name + " name");
            return;
        }
        s.source.Stop();
    }
}
