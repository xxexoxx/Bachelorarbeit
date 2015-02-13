using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    // Public Variables
    public GameObject PrefabLivingArea;
    public int amountOfLivingAreas;
    public GameObject ParentLivingArea;

	// Use this for initialization
	void Start () {
        CreatePlanet();
	}

    // Create entire planet from scratch
    public void CreatePlanet()
    {
        float tmpDegree = 360f / amountOfLivingAreas;

        for (int i = 0; i < amountOfLivingAreas; i++)
        {
            GameObject tmpGO = (GameObject)Instantiate(PrefabLivingArea);

            tmpGO.name = "LivingArea" + i.ToString();

            // Reset parent and position
            tmpGO.transform.parent = ParentLivingArea.transform;
            tmpGO.transform.localPosition = Vector2.zero;

            // Calculate rotation
            tmpGO.transform.localEulerAngles = new Vector3(0, 0, i * tmpDegree);
            
        }
    }

}
