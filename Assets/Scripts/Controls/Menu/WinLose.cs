using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinLose : MonoBehaviour {

    // Public Variables
    public GameObject GameMenu;
    public Image Smiley;
    public Text Message;
    public Sprite Happy, Sad;

    public void CheckWin(bool playerWon)
    {
        GameMenu.GetComponent<GameMenu>().ChangeMenu(GameMenuStates.DetailsTree);

        gameObject.SetActive(true);

        if (playerWon)
        {
            Smiley.sprite = Happy;
            Message.text = "Congratulations";
        }
        else
        {
            Smiley.sprite = Sad;
            Message.text = "Game Over";
        }
    }

    public void RestartGame()
    {
        Resources.UnloadUnusedAssets();
        Application.LoadLevel(0);
    }
}
