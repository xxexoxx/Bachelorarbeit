using UnityEngine;
using System.Collections;

public class StartAnimation : MonoBehaviour {

    // private Variables
    private Planet PlanetScript;

    void Start()
    {
        PlanetScript = GameObject.Find(Planet.GetPlanetName()).GetComponent<Planet>();
        PlanetScript.AddAnimationObject(this);
    }

    public void StartAllAnimations(AudiovisualEffects TypeOfAudiovisualEffects)
    {
        SendMessage("Animate", TypeOfAudiovisualEffects, SendMessageOptions.DontRequireReceiver);
    }
}
