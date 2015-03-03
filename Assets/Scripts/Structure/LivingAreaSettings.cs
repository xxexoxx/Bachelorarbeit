using UnityEngine;
using System.Collections;

[System.Serializable]
public class LivingAreaSettings {

    public string[] lightCondition;
    public bool[] lightShow;
    public Color lightColor;
    [Range(0, 1)]
    public float chanceOfCity;
    public string[] zoneName;
    public Sprite[] zoneSprites;
    public string[] humidityName;
    public Color[] humidityColor;
    public string[] phValueName;
    public Color[] phValueColor;
}
