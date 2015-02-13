using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {

    // Public Variables
    public GameObject MenuRestartGame;

    // Open restart game dialog
    public void OpenRestartGameDialog()
    {
        MenuRestartGame.SetActive(true);
    }
    // Close restart game dialog
    public void CloseRestartGameDialog()
    {
        MenuRestartGame.SetActive(false);
    }

    // Restart game
    public void RestartGame()
    {
        Resources.UnloadUnusedAssets();
        Application.LoadLevel(0);
    }
}
