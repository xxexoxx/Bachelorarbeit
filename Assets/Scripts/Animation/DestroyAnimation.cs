using UnityEngine;
using System.Collections;

public class DestroyAnimation : MonoBehaviour, IAnimation {

    public void Animate(AudiovisualEffects TypeOfAudiovisualEffects)
    {
        if (TypeOfAudiovisualEffects == AudiovisualEffects.Off)
        {
            Destroy(gameObject);
        }
    }
}
