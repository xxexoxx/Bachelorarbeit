using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LivingArea : MonoBehaviour {

    // Public Variables
    public SpriteRenderer LightRenderer, CityRenderer, ZoneRenderer, HumidityRenderer, PhValueRenderer, SectionRenderer;
    public GameObject TreeGO;

    // Private Variables
    private string currentLight;
    private bool currentCity;
    private string currentZone;
    private string currentHumidity;
    private string currentPhValue;

    private bool movedFlag;
    private Planet PlanetScript;
    private bool treeExists;
    private GameObject SelectedGameObject;
    private Global GlobalScript;
    private Color standardSliceColor;

    void Start()
    {
        PlanetScript = GameObject.Find(Planet.GetPlanetName()).GetComponent<Planet>();
        GlobalScript = GameObject.Find(Planet.GetPlanetName()).GetComponent<Global>();
        standardSliceColor = SectionRenderer.color;
        if (PlanetScript != null)
        {
            // Initialize LivingArea
            RandomInitialize();
        } 
    }

    void Update()
    {
        if (PlanetScript.GetCanControl())
        {
            SelectLivingArea();
        }
    }

    // Initialize
    public void RandomInitialize()
    {
        // Light
        if (PlanetScript.GetGlobalLivingAreaSettings().lightCondition.Length > 0 && PlanetScript.GetGlobalLivingAreaSettings().lightShow.Length > 0 && PlanetScript.GetGlobalLivingAreaSettings().lightCondition.Length == PlanetScript.GetGlobalLivingAreaSettings().lightShow.Length)
        {
            int random = Random.Range(0, PlanetScript.GetGlobalLivingAreaSettings().lightCondition.Length);
            currentLight = PlanetScript.GetGlobalLivingAreaSettings().lightCondition[random];

            if (PlanetScript.GetGlobalLivingAreaSettings().lightShow[random])
            {
                LightRenderer.color = PlanetScript.GetColorLight();
                LightRenderer.enabled = true;
            }
            else
            {
                LightRenderer.enabled = false;
            }
            
        }        

        // City
        if (Random.value < PlanetScript.GetGlobalLivingAreaSettings().chanceOfCity)
        {
            currentCity = true;
        }
        else
        {
            currentCity = false;
        }
        CityRenderer.enabled = currentCity;

        // Zone
        if (PlanetScript.GetGlobalLivingAreaSettings().zoneName.Length > 0 && PlanetScript.GetGlobalLivingAreaSettings().zoneSprites.Length > 0 && PlanetScript.GetGlobalLivingAreaSettings().zoneName.Length == PlanetScript.GetGlobalLivingAreaSettings().zoneSprites.Length)
        {
            currentZone = PlanetScript.GetGlobalLivingAreaSettings().zoneName[Random.Range(0, PlanetScript.GetGlobalLivingAreaSettings().zoneName.Length)];
            ZoneRenderer.sprite = PlanetScript.GetSpriteZone(currentZone);
        }

        // Humidity
        if (PlanetScript.GetGlobalLivingAreaSettings().humidityName.Length > 0 && PlanetScript.GetGlobalLivingAreaSettings().humidityColor.Length > 0 && PlanetScript.GetGlobalLivingAreaSettings().humidityName.Length == PlanetScript.GetGlobalLivingAreaSettings().humidityColor.Length)
        {
            currentHumidity = PlanetScript.GetGlobalLivingAreaSettings().humidityName[Random.Range(0, PlanetScript.GetGlobalLivingAreaSettings().humidityName.Length)];
            HumidityRenderer.color = PlanetScript.GetColorHumidity(currentHumidity);
        }

        // PhValue
        if (PlanetScript.GetGlobalLivingAreaSettings().phValueName.Length > 0 && PlanetScript.GetGlobalLivingAreaSettings().phValueColor.Length > 0 && PlanetScript.GetGlobalLivingAreaSettings().phValueName.Length == PlanetScript.GetGlobalLivingAreaSettings().phValueColor.Length)
        {
            currentPhValue = PlanetScript.GetGlobalLivingAreaSettings().phValueName[Random.Range(0, PlanetScript.GetGlobalLivingAreaSettings().phValueName.Length)];
            PhValueRenderer.color = PlanetScript.GetColorPhValue(currentPhValue);
        }
    }


    // Plant a tree
    // returns if the tree has been planted
    public bool PlantTree(TreeSettings tmpTree)
    {
        if(treeExists == false)
        {
            treeExists = true;
            TreeGO.GetComponent<TreeObject>().Initialize(tmpTree);

            return false;
        }
        return true;
    }

    // Open Tree Detail
    public void RemoveTree()
    {
        TreeGO.GetComponent<TreeObject>().RemoveTree();
        treeExists = false;
    }

    public float[] EvaluateTree()
    {
        if (treeExists)
        {
            return TreeGO.GetComponent<TreeObject>().EvaluateTree(currentLight, currentCity, currentZone, currentHumidity, currentPhValue);
        }
        return new float[] { 0, 0 };
    }

    void SelectLivingArea()
    {
        if (Input.touchCount == 1 && treeExists)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), new Vector3(0, 0, 1));

                if (hit.Length > 0)
                {
                    for (int i = 0; i < hit.Length; i++)
                    {
                        if (hit[i].collider.tag == "LivingArea" && GlobalScript.GetEventSystem().currentSelectedObject == null)
                        {
                            SelectedGameObject = hit[i].transform.gameObject;
                        }
                    }
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                movedFlag = true;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (!movedFlag)
                {
                    RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), new Vector3(0, 0, 1));

                    if (hit.Length > 0)
                    {
                        for (int i = 0; i < hit.Length; i++)
                        {
                            if (hit[i].collider.tag == "LivingArea")
                            {
                                if (hit[i].collider.gameObject == SelectedGameObject && hit[i].transform == transform)
                                {
                                    DetailsMenu tmpMenu = GlobalScript.GetDetailsTree().GetComponent<DetailsMenu>();

                                    tmpMenu.UpdateDetails(gameObject);

                                    tmpMenu.OpenDetailsTree();
                                }
                            }
                        }
                    }
                }
                SelectedGameObject = null;
                movedFlag = false;
            }
        }
    }

    public void HighlightArea(bool highlight)
    {
        if (highlight)
        {
            SectionRenderer.color = new Color(1,0.5f,0, 1);
            SectionRenderer.sortingLayerName = "Tree";
        }
        else
        {
            SectionRenderer.color = standardSliceColor;
            SectionRenderer.sortingLayerName = "LivingArea";
        }
    }

    public bool GetCity()
    {
        return currentCity;
    }

    public string GetZone()
    {
        return currentZone;
    }

}
