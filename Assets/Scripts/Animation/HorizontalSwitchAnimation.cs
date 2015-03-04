using UnityEngine;
using System.Collections;

public class HorizontalSwitchAnimation : MonoBehaviour, IAnimation {

    public void Animate(AudiovisualEffects TypeOfAudiovisualEffects)
    {
        if (TypeOfAudiovisualEffects == AudiovisualEffects.On)
        {
            if (Random.value > 0.5f)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            }
        }
    }
}
