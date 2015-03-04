using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum AudiovisualEffects { Off, On };

public class Planet : MonoBehaviour {

    // Static Variables
    private static string staticPlanetName;

    // Public Variables
    public GameObject PrefabLivingArea;
    public int amountOfLivingAreas;
    public GameObject ParentLivingArea;
    public Text greenMeterUI, EnergyBarUI;
    public float greenMeter, energyBar;

    // Private Variables
    private bool canControl;
    private AudiovisualEffects TypeOfAudiovisualEffects;
    private List<StartAnimation> AnimationObjectList = new List<StartAnimation>();

    [SerializeField]
    private LivingAreaSettings GlobalLivingAreaSettings;
    [SerializeField]
    private TreeSettings[] GlobalTreeSettings;

    void Awake()
    {
        staticPlanetName = gameObject.name;

        UpdateGreenEnergy();
    }

	// Use this for initialization
	void Start () {
        canControl = false;

        CreatePlanet();
	}

    // Get the planet's gameobject name
    public static string GetPlanetName()
    {
        return staticPlanetName;
    }

    // Create entire planet from scratch
    public void CreatePlanet()
    {
        float tmpDegree = 360f / amountOfLivingAreas;

        for (int i = 0; i < amountOfLivingAreas; i++)
        {
            GameObject tmpGO = (GameObject)Instantiate(PrefabLivingArea);

            tmpGO.name = "LivingArea" + i.ToString();

            // Reset parent and position
            tmpGO.transform.parent = ParentLivingArea.transform;
            tmpGO.transform.localPosition = Vector2.zero;

            // Calculate rotation
            tmpGO.transform.localEulerAngles = new Vector3(0, 0, i * tmpDegree);
            
        }
    }

    // Add new object to the animation list
    public void AddAnimationObject(StartAnimation obj)
    {
        AnimationObjectList.Add(obj);
    }

    // Getter and Setter
    #region canControl
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
    #endregion

    #region TypeOfEffectIntensity
    // Getter
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
                AnimationObjectList[i].StartAllAnimations(TypeOfAudiovisualEffects);
            }
        }
    }
    #endregion

    public void IncreaseEnergy(int value)
    {
        energyBar += value;

        UpdateGreenEnergy();
    }

    public bool ReduceEnergy(int cost)
    {
        if (energyBar - cost >= 0)
        {
            energyBar -= cost;

            UpdateGreenEnergy();

            return true;
        }
        UpdateGreenEnergy();
        return false;
    }

    public void EvaluatePlanet()
    {
        greenMeter = 0;

        foreach (Transform livingarea in ParentLivingArea.transform)
        {
            float[] tmpRewards = livingarea.GetComponent<LivingArea>().EvaluateTree();

            greenMeter += tmpRewards[0];
            energyBar += tmpRewards[1];
        }

        UpdateGreenEnergy();
    }

    public float GetGreenMeter()
    {
        return greenMeter;
    }

    void UpdateGreenEnergy()
    {
        greenMeterUI.text = greenMeter.ToString() + "%";
        EnergyBarUI.text = energyBar.ToString();
    }

    // Living Area
    #region GlobalLivingAreaSettings
    public LivingAreaSettings GetGlobalLivingAreaSettings()
    {
        return GlobalLivingAreaSettings;
    }
    #endregion

    #region Light
    // Light
    public Color GetColorLight()
    {
        return GlobalLivingAreaSettings.lightColor;
    }
    #endregion

    #region Zone
    // Getter
    // Returns a sprite if the sent string exists in the array
    public Sprite GetSpriteZone(string value)
    {
        for (int i = 0; i < GlobalLivingAreaSettings.zoneName.Length; i++)
        {
            if (GlobalLivingAreaSettings.zoneName[i] == value)
            {
                return GlobalLivingAreaSettings.zoneSprites[i];
            }
        }

        return null;
    }
    #endregion

    #region Humidity
    // Getter
    // Returns a color if the sent string exists in the array
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
    #endregion

    #region PhValue
    // Getter
    // Returns a color if the sent string exists in the array
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
    #endregion

    // Tree
    public TreeSettings GetTreeById(int id)
    {
        return GlobalTreeSettings[id];
    }

}
