using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AudiovisualEffects { Off, On };

public class GlobalAdmin : MonoBehaviour {

    // Public Variables
    

    //Private Variables

    // Serialized
    [SerializeField]
    private LivingAreaSettings GlobalLivingAreaSettings;

    // Hidde
    private bool canControl;
    private AudiovisualEffects TypeOfAudiovisualEffects;
    [SerializeField]
    private List<GameObject> AnimationObjectList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        canControl = false;
	}

    // GlobalLivingAreaSettings
    public LivingAreaSettings GetGlobalLivingAreaSettings()
    {
        return GlobalLivingAreaSettings;
    }

    // canControl
    // Getter
    public bool GetCanControl()
    {
        return canControl;
    }
    // Setter
    public void SetCanControl(bool value)
    {
        canControl = value;
    }

    // TypeOfEffectIntensity
    public AudiovisualEffects GetEffectIntensity()
    {
        return TypeOfAudiovisualEffects;
    }
    // Setter
    public void SetEffectIntensity(AudiovisualEffects value, bool animateList)
    {
        TypeOfAudiovisualEffects = value;

        if (animateList)
        {
            for (int i = 0; i < AnimationObjectList.Count; i++)
            {
                AnimationObjectList[i].GetComponent<StartAnimation>().StartAllAnimations(TypeOfAudiovisualEffects);
            }
        }
    }
    
    // Colors
    // Light
    public Color GetColorLight()
    {
        return GlobalLivingAreaSettings.lightColor;
    }

    // Humidity
    public Color GetColorHumidity(string value)
    {
        for (int i = 0; i < GlobalLivingAreaSettings.humidityName.Length; i++)
        {
            if (GlobalLivingAreaSettings.humidityName[i] == value)
            {
                return GlobalLivingAreaSettings.humidityColor[i];
            }
        }

        return Color.white;
    }

    // PhValue
    public Color GetColorPhValue(string value)
    {
        for (int i = 0; i < GlobalLivingAreaSettings.phValueName.Length; i++)
        {
            if (GlobalLivingAreaSettings.phValueName[i] == value)
            {
                return GlobalLivingAreaSettings.phValueColor[i];
            }
        }

        return Color.white;
    }

    // AnimationObjects
    public void AddAnimationObject(GameObject obj)
    {
        AnimationObjectList.Add(obj);
    }
}
