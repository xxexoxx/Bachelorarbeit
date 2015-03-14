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
    private int currentID;

    void Start()
    {
        PlanetScript = GameObject.Find(Planet.GetPlanetName()).GetComponent<Planet>();
        currentID = 0;

        UpdateDetail(0);
    }

    // Open the tree menu
    public void OpenTreeMenu()
    {
        GameMenu.GetComponent<GameMenu>().ChangeMenu(GameMenuStates.TreeMenu);
        UpdateDetail(currentID);
        TreeListMenu.SetActive(true);
    }

    // Close the tree menu
    public void CloseTreeMenu(bool reset = true)
    {
        if (reset)
        {
            GameMenu.GetComponent<GameMenu>().ChangeMenu(GameMenuStates.MainMenu);
        }
        TreeListMenu.SetActive(false);
    }

    // Change Detail part
    public void UpdateDetail(int id)
    {
        currentID = id;

        currentTree = PlanetScript.GetTreeById(id);

        DetailTreeImage.sprite = currentTree.FullyGrown;

        Color tmpColor = Color.black;
        if (PlanetScript.GetEnergy() < currentTree.energyCost)
        {
            tmpColor = Color.red;
        }
        DetailEnergy.text = currentTree.energyCost.ToString();
        DetailEnergy.color = tmpColor;

        DetailGreenMeter.text = currentTree.greenMeter.ToString() + "%";
        DetailEnglishName.text = currentTree.englishName;
        DetailLatinName.text = currentTree.latinName;
    }

    // Plant tree
    public void PlantCurrentTree()
    {
        if (PlanetScript.ReduceEnergy(currentTree.energyCost))
        {
            GameMenu.GetComponent<GameMenu>().ChangeMenu(GameMenuStates.DetailsTree, true);

            GameObject tmpObj = (GameObject)Instantiate(PrefabMovableTreeIcon);
            tmpObj.transform.position = (Vector2)PlanetScript.gameObject.transform.position + new Vector2(0, 7);
            tmpObj.transform.parent = PlanetScript.transform;
            tmpObj.GetComponent<MovableTreeIcon>().Initialize(currentTree, GameMenu);

            CloseTreeMenu(false);
        }
    }

}
