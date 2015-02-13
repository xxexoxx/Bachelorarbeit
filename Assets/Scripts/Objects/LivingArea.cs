using UnityEngine;
using System.Collections;

public class LivingArea : MonoBehaviour {

    // Public Variables
    public SpriteRenderer LightRenderer, HumidityRenderer, PhValueRenderer;

    // Private Variables 
    private GlobalAdmin GlobalAdminScript;
    private bool currentLight;
    private string currentHumidity;
    private string currentPhValue;

    void Start()
    {
        GlobalAdminScript = GameObject.Find("GlobalAdmin").GetComponent<GlobalAdmin>();

        // Initialize LivingArea
        RandomInitialize();
        
    }

    // Initialize
    public void RandomInitialize()
    {
        // Light
        LightRenderer.color = GlobalAdminScript.GetColorLight();
        if (Random.value > GlobalAdminScript.GetGlobalLivingAreaSettings().chanceOfLight)
        {
            LightRenderer.enabled = false;

        }

        // Humidity
        if (GlobalAdminScript.GetGlobalLivingAreaSettings().humidityName.Length > 0 && GlobalAdminScript.GetGlobalLivingAreaSettings().humidityColor.Length > 0 && GlobalAdminScript.GetGlobalLivingAreaSettings().humidityName.Length == GlobalAdminScript.GetGlobalLivingAreaSettings().humidityColor.Length)
        {
            currentHumidity = GlobalAdminScript.GetGlobalLivingAreaSettings().humidityName[Random.Range(0, GlobalAdminScript.GetGlobalLivingAreaSettings().humidityName.Length)];
            HumidityRenderer.color = GlobalAdminScript.GetColorHumidity(currentHumidity);
        }

        // PhValue
        if (GlobalAdminScript.GetGlobalLivingAreaSettings().phValueName.Length > 0 && GlobalAdminScript.GetGlobalLivingAreaSettings().phValueColor.Length > 0 && GlobalAdminScript.GetGlobalLivingAreaSettings().phValueName.Length == GlobalAdminScript.GetGlobalLivingAreaSettings().phValueColor.Length)
        {
            currentPhValue = GlobalAdminScript.GetGlobalLivingAreaSettings().phValueName[Random.Range(0, GlobalAdminScript.GetGlobalLivingAreaSettings().phValueName.Length)];
            PhValueRenderer.color = GlobalAdminScript.GetColorPhValue(currentPhValue);
        }
    }
}
