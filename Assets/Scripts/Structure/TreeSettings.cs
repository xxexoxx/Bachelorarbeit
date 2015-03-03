using UnityEngine;
using System.Collections;

[System.Serializable]
public class TreeSettings
{
    public Sprite Planted, FullyGrown, Dead, Icon;
    public string englishName, latinName;
    public string[] light;
    public bool cityCompatibility;
    public string[] zone;
    public string[] humidity;
    public string[] phvalue;
    public int greenMeter;
    public int energyCost;
    public int energyReward;
        
}
