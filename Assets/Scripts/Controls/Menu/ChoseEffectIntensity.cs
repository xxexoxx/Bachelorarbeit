using UnityEngine;
using System.Collections;

public class ChoseEffectIntensity : MonoBehaviour {

    // private Variables
    private Planet PlanetScript;

	// Use this for initialization
	void Start () {
        PlanetScript = GameObject.Find(Planet.GetPlanetName()).GetComponent<Planet>();
	}

    // Set effect Intensity
    public void SetEffectIntensity(int value)
    {
        AudiovisualEffects tmpIntensity = AudiovisualEffects.Off;

        switch (value)
        {
            case 0: tmpIntensity = AudiovisualEffects.Off;
                break;
            case 1: tmpIntensity = AudiovisualEffects.On;
                break;
        }

        // Set effect intensity
        PlanetScript.SetEffectIntensity(tmpIntensity, true);
        // Activate in game controls
        PlanetScript.SetCanControl(true);

        // Disable the effect intensity menu
        gameObject.SetActive(false);
    }
}
