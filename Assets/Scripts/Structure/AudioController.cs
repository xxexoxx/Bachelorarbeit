using UnityEngine;
using System.Collections;

public enum SoundTypes { PlantTreeSuccess, PlantTreeFail, RemoveTree, NextYear};

public class AudioController : MonoBehaviour {

    // Public Variables
    public AudioClip Music, PlantTreeSuccess, PlantTreeFail, RemoveTree, NextYear, ButtonClick;

    // Private Variables
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = gameObject.AddComponent<AudioSource>();
	}

    public void PlaySound(SoundTypes type)
    {
        if (GetComponent<Planet>().GetEffectIntensity() == AudiovisualEffects.On)
        {
            switch (type)
            {
                case SoundTypes.PlantTreeSuccess:
                    audio.PlayOneShot(PlantTreeSuccess);
                    break;
                case SoundTypes.PlantTreeFail:
                    audio.PlayOneShot(PlantTreeFail);
                    break;
                case SoundTypes.RemoveTree:
                    audio.PlayOneShot(RemoveTree);
                    break;
                case SoundTypes.NextYear:
                    audio.PlayOneShot(NextYear);
                    break;
            }
        }

    }

    public void PlayMusic()
    {
        if (GetComponent<Planet>().GetEffectIntensity() == AudiovisualEffects.On)
        {
            AudioSource tmpMusic = gameObject.AddComponent<AudioSource>();
            tmpMusic.clip = Music;
            tmpMusic.loop = true;
            tmpMusic.Play();
        }
        
    }

    public void Button()
    {
        if (GetComponent<Planet>().GetEffectIntensity() == AudiovisualEffects.On)
        {
            audio.PlayOneShot(ButtonClick);
        }  
    }
}
