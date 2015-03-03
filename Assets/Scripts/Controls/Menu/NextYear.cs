using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextYear : MonoBehaviour {

    // Public Variables
    public int automaticIncrease, roundsAmount;
    public Text RoundsUI;


    // Private Variables
    private Planet PlanetScript;

    void Start()
    {
        PlanetScript = GameObject.Find(Planet.GetPlanetName()).GetComponent<Planet>();
        RoundsUI.text = roundsAmount.ToString();
    }

    public void AdvanceToNextYear()
    {
        // Increase Energy automatically
        PlanetScript.IncreaseEnergy(automaticIncrease);
        RoundsUI.text = (--roundsAmount).ToString();

        // Evaluate Trees
        PlanetScript.EvaluatePlanet();
    }
}
