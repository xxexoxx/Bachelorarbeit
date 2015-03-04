using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DetailsMenu : MonoBehaviour {

    // Public Variables
    public GameObject GameMenu;
    public Text treeName;
    public Image treeImage, treeSmiley;

    // Private Veriables
    private GameObject LivingArea;


	public void UpdateDetails(GameObject LA)
    {
        LivingArea = LA;

        TreeSettings tmpTree = LivingArea.GetComponent<LivingArea>().TreeGO.GetComponent<TreeObject>().GetTree();

        treeName.text = tmpTree.latinName;
        treeImage.sprite = tmpTree.FullyGrown;
        treeSmiley.sprite = LivingArea.GetComponent<LivingArea>().TreeGO.GetComponent<TreeObject>().GetSmiley();
    }

    // Open the details menu
    public void OpenDetailsTree()
    {
        GameMenu.GetComponent<GameMenu>().ChangeMenu(GameMenuStates.DetailsTree);
        gameObject.SetActive(true);
    }

    // Close the detail menu
    public void CloseDetailsTree()
    {
        GameMenu.GetComponent<GameMenu>().ChangeMenu(GameMenuStates.MainMenu);
        gameObject.SetActive(false);
    }

    public void RemoveTree()
    {
        LivingArea.GetComponent<LivingArea>().RemoveTree();
        CloseDetailsTree();
    }
}
