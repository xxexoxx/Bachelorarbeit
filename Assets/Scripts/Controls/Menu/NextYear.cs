using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextYear : MonoBehaviour {

    // Public Variables
    public WinLose WinLoseScript;
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
        // Sound
        PlanetScript.GetComponent<AudioController>().PlaySound(SoundTypes.NextYear);

        // Increase Energy automatically
        PlanetScript.IncreaseEnergy(automaticIncrease);
        RoundsUI.text = (--roundsAmount).ToString();

        // Evaluate Trees
        PlanetScript.EvaluatePlanet();

        if (PlanetScript.GetGreenMeter() >= 100)
        {
            WinLoseScript.CheckWin(true);
        }
        else
        {
            if (roundsAmount == 0)
            {
                WinLoseScript.CheckWin(false);
            }
        }

    }
}
