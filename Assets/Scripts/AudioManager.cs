using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  //  private AudioSource bgSource;
    private AudioSource[] audioSource;

    public AudioClipManager[] audioClips;

    private void Awake()
    {
        //  bgSource = GetComponent<AudioSource>();

        audioSource = new AudioSource[audioClips.Length];

        for (int i = 0; i < audioClips.Length; i++)
        {
            gameObject.AddComponent<AudioSource>();
        }
        audioSource = gameObject.GetComponents<AudioSource>();

        for (int i = 0; i < audioClips.Length; i++)
        {
            audioSource[i].playOnAwake = audioClips[i].playOnAwake;
            audioSource[i].clip = audioClips[i].audioClip;
            audioSource[i].loop = audioClips[i].loop;
            //audioSource[i].volume = audioClips[i].volume;
            audioClips[i].source = audioSource[i];
        }
    }
  

   public void PlaySound(string _name)
    {
        foreach(var item in audioClips)
        {
            if(item.name == _name)
            {
                item.source.Play();
                return;
            }
        }
    }

   

 
    public void MusicPlayPause(bool _play)
    {
        if (_play)
        {
            audioClips[audioClips.Length - 1].source.Play();
        }
        else
        {
            audioClips[audioClips.Length - 1].source.Pause();
        }
    }

    public void MusicVolume(float _volume)
    {
        audioClips[audioClips.Length - 1].source.volume = _volume;
    }
    public void MainSoundVolue(float _volume)
    {
        for(int i = 0;i<audioClips.Length;i++)
        {
            audioClips[i].source.volume = _volume;
        }
    }
    
}

[System.Serializable]
public class AudioClipManager
{
    public string name;
    public AudioClip audioClip;
    public bool loop;
    public bool playOnAwake;
    [Range(0f,1f)]
    //public float volume;
    public AudioSource source;
}
