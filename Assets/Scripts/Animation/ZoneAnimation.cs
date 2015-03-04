using UnityEngine;
using System.Collections;

public class ZoneAnimation : MonoBehaviour {

    public GameObject LivingArea;
    public string Zone;

    public void Animate(AudiovisualEffects TypeOfAudiovisualEffects)
    {
        if (TypeOfAudiovisualEffects == AudiovisualEffects.On)
            {
                if (Random.value > .8f)
                {
                    //Destroy(gameObject);
                }
                else
                {
                    string tmpZone = LivingArea.GetComponent<LivingArea>().GetZone();

                    if (tmpZone == Zone)
                    {
                        GetComponent<Animation>().Play();
                    }

                }
            }
    }
}
