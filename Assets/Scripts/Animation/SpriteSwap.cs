using UnityEngine;
using System.Collections;

public class SpriteSwap : MonoBehaviour, IAnimation {

    // Public Variables
    public Sprite AudiovisualEffectOn, AudiovisualEffectOff;

    public void Animate(AudiovisualEffects TypeOfAudiovisualEffects) 
    {

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (TypeOfAudiovisualEffects == AudiovisualEffects.On)
        {
            spriteRenderer.sprite = AudiovisualEffectOn;
        }
        else
        {
            spriteRenderer.sprite = AudiovisualEffectOff;
        }
	}
}
