using UnityEngine;
using System.Collections;

public class SettingsMenu : MonoBehaviour {

    // Public Variables
    public GameObject GameMenu, MenuRestartGame;

    // Open restart game dialog
    public void OpenRestartGameDialog()
    {
        MenuRestartGame.SetActive(true);
        GameMenu.GetComponent<GameMenu>().ChangeMenu(GameMenuStates.SettingsMenu);
    }
    // Close restart game dialog
    public void CloseRestartGameDialog()
    {
        GameMenu.GetComponent<GameMenu>().ChangeMenu(GameMenuStates.MainMenu);
        MenuRestartGame.SetActive(false);
    }

    // Restart game
    public void RestartGame()
    {
        Resources.UnloadUnusedAssets();
        Application.LoadLevel(0);
    }

    // End game
    public void CloseApp()
    {
        Application.Quit();
    }
}
