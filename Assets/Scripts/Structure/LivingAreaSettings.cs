using UnityEngine;
using System.Collections;

[System.Serializable]
public class LivingAreaSettings {

    [Range(0, 1)]
    public float chanceOfLight;
    public Color lightColor;
    public string[] humidityName;
    public Color[] humidityColor;
    public string[] phValueName;
    public Color[] phValueColor;
}
