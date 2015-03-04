using UnityEngine;
using System.Collections;

public class CloudGenerator : MonoBehaviour {

    public GameObject PrefabCloud;
    public float minDis, maxDis, minScale, maxScale, amountOfClouds;  

    void Start()
    {
        GenerateClouds();
    }

    void GenerateClouds()
    {
        float tmpDegree = 360f / amountOfClouds;

        for (int i = 0; i < amountOfClouds; i++)
        {
            GameObject tmpGO = (GameObject)Instantiate(PrefabCloud);

            // Reset parent and position
            tmpGO.transform.parent = transform;
            tmpGO.transform.GetChild(0).transform.localPosition = new Vector2(0, Random.RandomRange(minDis, maxDis));
            float tmpScale = Random.RandomRange(minScale, maxScale);
            tmpGO.transform.GetChild(0).transform.localScale = new Vector3(tmpScale, tmpScale, 1);

            // Calculate rotation
            tmpGO.transform.localEulerAngles = new Vector3(0, 0, i * tmpDegree);
            
        }
    }
}
