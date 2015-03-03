using UnityEngine;
using System.Collections;

public enum GameMenuStates { MainMenu, TreeMenu, SettingsMenu, DetailsTree};

public class GameMenu : MonoBehaviour {

	// Public Variables
    public GameObject PlantTree, SettingsMenu, NextYearButton;

    // Private Variables
    private Planet PlanetScript;

    void Start()
    {
        PlanetScript = GameObject.Find(Planet.GetPlanetName()).GetComponent<Planet>();
    }

    // Change UI LAyout
    public void ChangeMenu(GameMenuStates menu)
    {
        switch (menu)
        {
            case GameMenuStates.MainMenu:
                PlanetScript.SetCanControl(true);
                PlantTree.SetActive(true);
                SettingsMenu.SetActive(true);
                NextYearButton.SetActive(true);
                break;
            case GameMenuStates.TreeMenu:
                PlanetScript.SetCanControl(false);
                SettingsMenu.SetActive(false);
                NextYearButton.SetActive(false);
                break;
            case GameMenuStates.SettingsMenu:
                PlanetScript.SetCanControl(false);
                PlantTree.SetActive(false);
                NextYearButton.SetActive(false);
                break;
            case GameMenuStates.DetailsTree:
                PlanetScript.SetCanControl(false);
                PlantTree.SetActive(false);
                SettingsMenu.SetActive(false);
                NextYearButton.SetActive(false);
                break;
        }
    }

}
