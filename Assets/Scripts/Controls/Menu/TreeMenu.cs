using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TreeMenu : MonoBehaviour {

    // Public Variables
    public GameObject GameMenu, TreeListMenu;

    // Tree details
    public GameObject PrefabMovableTreeIcon;
    public Image DetailTreeImage;
    public Text DetailEnergy, DetailGreenMeter, DetailEnglishName, DetailLatinName;

    // private Variables
    private Planet PlanetScript;
    private TreeSettings currentTree;

    void Start()
    {
        PlanetScript = GameObject.Find(Planet.GetPlanetName()).GetComponent<Planet>();
        UpdateDetail(0);
    }

    // Open the tree menu
    public void OpenTreeMenu()
    {
        GameMenu.GetComponent<GameMenu>().ChangeMenu(GameMenuStates.TreeMenu);
        TreeListMenu.SetActive(true);
    }

    // Close the tree menu
    public void CloseTreeMenu()
    {
        GameMenu.GetComponent<GameMenu>().ChangeMenu(GameMenuStates.MainMenu);
        TreeListMenu.SetActive(false);
    }

    // Change Detail part
    public void UpdateDetail(int id)
    {
        currentTree = PlanetScript.GetTreeById(id);

        DetailTreeImage.sprite = currentTree.FullyGrown;
        DetailEnergy.text = currentTree.energyCost.ToString();
        DetailGreenMeter.text = currentTree.greenMeter.ToString() + "%";
        DetailEnglishName.text = currentTree.englishName;
        DetailLatinName.text = currentTree.latinName;
    }

    // Plant tree
    public void PlantCurrentTree()
    {
        if (PlanetScript.ReduceEnergy(currentTree.energyCost))
        {
            GameObject tmpObj = (GameObject)Instantiate(PrefabMovableTreeIcon);
            tmpObj.transform.position = (Vector2)PlanetScript.gameObject.transform.position + new Vector2(0, 7);
            tmpObj.transform.parent = PlanetScript.transform;
            tmpObj.GetComponent<MovableTreeIcon>().Initialize(currentTree);

            CloseTreeMenu();
        }
    }

}
