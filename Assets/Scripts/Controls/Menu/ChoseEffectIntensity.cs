using UnityEngine;
using System.Collections;

public class ChoseEffectIntensity : MonoBehaviour {

    // private Variables
    private GlobalAdmin GlobalAdminScript;

	// Use this for initialization
	void Start () {
        GlobalAdminScript = GameObject.Find("GlobalAdmin").GetComponent<GlobalAdmin>();
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
        GlobalAdminScript.SetEffectIntensity(tmpIntensity, true);
        // Activate in game controls
        GlobalAdminScript.SetCanControl(true);

        // Disable the effect intensity menu
        gameObject.SetActive(false);
    }
}
