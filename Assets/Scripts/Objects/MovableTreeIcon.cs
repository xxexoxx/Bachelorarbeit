using UnityEngine;
using System.Collections;

public class MovableTreeIcon : MonoBehaviour {

    // Public Variables
    public SpriteRenderer TreeIconRenderer;

    // Private Variables
    private GameObject PlanetGO;
    private bool movingTree;
    private TreeSettings tree;
    private Planet PlanetScript;
    private GameObject GameMenuGO;

	// Use this for initialization
	void Start () {
        PlanetGO = GameObject.Find(Planet.GetPlanetName());
        PlanetScript = PlanetGO.GetComponent<Planet>();
        movingTree = false;
	}

    public void Initialize(TreeSettings tmpTree, GameObject obj)
    {
        tree = tmpTree;
        GameMenuGO = obj;
        TreeIconRenderer.sprite = tree.FullyGrown;
    }
	
	// Update is called once per frame
	void Update () {

        if (PlanetScript.GetCanControl())
        {
            MoveTree();
        }
	}

    void MoveTree()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Input.GetTouch(0).position);

                if (hit.collider && hit.collider.tag == "MovableTreeIcon")
                {
                    movingTree = true;
                    collider2D.enabled = false;
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (movingTree)
                {
                    transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                    /*Vector3 source = transform.localPosition;
                    Vector3 target = PlanetGO.transform.position;

                    float angle = Mathf.DeltaAngle(Mathf.Atan2(source.y, source.x) * Mathf.Rad2Deg, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);

                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -angle - 180);*/

                    //transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
               

                if (movingTree)
                {
                    RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), new Vector3(0, 0, 1));

                    if (hit.Length > 0)
                    {
                        for (int i = 0; i < hit.Length; i++)
                        {
                            if (hit[i].collider.tag == "LivingArea")
                            {
                                // Returns if a tree exists in that living area
                                if (!hit[i].collider.GetComponent<LivingArea>().PlantTree(tree))
                                {
                                    GameMenuGO.GetComponent<GameMenu>().ChangeMenu(GameMenuStates.MainMenu);
                                    Destroy(gameObject);
                                }
                                else
                                {
                                    transform.position = PlanetGO.transform.position + (-PlanetGO.transform.position + transform.position).normalized * 7;

                                    PlanetGO.GetComponent<AudioController>().PlaySound(SoundTypes.PlantTreeFail);
                                }
                            }
                            
                        }
                    }
                }

                collider2D.enabled = true;
                movingTree = false;
            }
        }
    }
}
