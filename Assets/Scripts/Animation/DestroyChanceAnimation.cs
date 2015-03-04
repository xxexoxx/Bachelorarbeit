using UnityEngine;
using System.Collections;

public class DestroyChanceAnimation : MonoBehaviour, IAnimation
{
    public GameObject LivingArea;

    public void Animate(AudiovisualEffects TypeOfAudiovisualEffects)
    {
        if (LivingArea.GetComponent<LivingArea>().GetCity())
        {
            if (TypeOfAudiovisualEffects == AudiovisualEffects.On)
            {
                if (Random.value > 0.4f)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
